using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QuotesParables.Models;
using QuotesParables.Utilities;
using QuotesParables.ViewModels;

namespace QuotesParables.Controllers
{
    public class QuotesController : Controller
    {
        private QuotesContext db = new QuotesContext();

        // GET: Quotes
        public ActionResult Index()
        {
            myGlobals.CurrentContext = "QuotesContext";
            QuoteListViewModel myViewModel = GetViewModelForList("Y");
            return View(myViewModel);
        }

        public ActionResult UnApproved()
        {
            myGlobals.CurrentContext = "QuotesContext";
            QuoteListViewModel myViewModel = GetViewModelForList("N");
            return View("Index",myViewModel);
        }

        private QuoteListViewModel GetViewModelForList(string approved)
        {
            string searchText;
            ReturnObject ro = new ReturnObject();
            QuoteListViewModel myViewModel = new QuoteListViewModel();
            myViewModel.searchCategoryId = getsearchCategoryId();
            myViewModel.searchQuoteTypeId = getSearchDataTypeId();
            myViewModel.searchText = getSessionSearchText();
            myViewModel.CategoryDropDown = db.Categories.OrderBy(x => x.Description).ToList();
            myViewModel.TypeDropDown = db.QuoteType.OrderBy(x => x.QuoteTypeDescription).ToList();
            IQueryable<Quote> mySet = db.Quotes;
            if (myViewModel.searchText != null && myViewModel.searchText.Trim().Length > 0)
            {
                searchText = myViewModel.searchText.Trim();
                mySet = mySet.Where(x => x.AuthorName.Contains(searchText) || x.QuoteText.Contains(searchText) || x.Contributor.Contains(searchText));
            }
            if (myViewModel.searchCategoryId > 0)
            {
                if (myViewModel.searchCategoryId == 33)
                {
                    mySet = mySet.Where(x => x.CategoryId == myViewModel.searchCategoryId
                    );
                }
                else
                {
                    mySet = mySet.Where(x => x.CategoryId == myViewModel.searchCategoryId
                    || x.CategoryId2 == myViewModel.searchCategoryId
                    || x.CategoryId3 == myViewModel.searchCategoryId
                    );
                }
            }
            if (myViewModel.searchQuoteTypeId > 0)
            {
                mySet = mySet.Where(x => x.QuoteTypeId == myViewModel.searchQuoteTypeId);
            }
            if (approved == "Y")
            {
                mySet = mySet.Where(x => x.Aprroved == "Y");
            }
            else
            {
                mySet = mySet.Where(x => x.Aprroved == "N");
            }
            mySet = mySet.Include(q => q.Category).Include(x => x.QuoteType).OrderByDescending(x => x.Likes);
            PagingParameters pagingParameters = getpagingParameters();
            pagingParameters.totalItemCount = mySet.Count();
            ro = PagingUtility.getPageSettings(pagingParameters);
            if (ro.IsValid)
            {
                pagingParameters = (PagingParameters)ro.ReturnObj1;
            }
            else
            {
                ViewBag.ErroMessages = ro.ErrorMessages;
            }
            myViewModel.quotesList = mySet.Skip(pagingParameters.newStart).Take(pagingParameters.newCount).ToList();
            SetSession.SetPagingParameters(pagingParameters);
            myViewModel.pagingParameters = pagingParameters;
            return myViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "searchCategoryId,searchText,searchQuoteTypeId")] QuoteListViewModel quoteListViewModel)
        {
            ReturnObject ro = new ReturnObject();
            PagingParameters pagingParameters = null;
            int searchCategoryId = quoteListViewModel.searchCategoryId;
            string searchText = quoteListViewModel.searchText;
            var myViewModel = new QuoteListViewModel();
            SetSession.SetSessionSearchCategoryID(searchCategoryId);
            SetSession.SetSessionSearchQuoteTypeID(quoteListViewModel.searchQuoteTypeId);
            SetSession.SetSessionSearchText(searchText);
            myViewModel.searchCategoryId = quoteListViewModel.searchCategoryId;
            myViewModel.searchText = quoteListViewModel.searchText;
            myViewModel.searchQuoteTypeId = quoteListViewModel.searchQuoteTypeId;
            myViewModel.CategoryDropDown = db.Categories.OrderBy(x => x.Description).ToList();
            myViewModel.TypeDropDown = db.QuoteType.OrderBy(x => x.QuoteTypeDescription).ToList();
            IQueryable<Quote> mySet = db.Quotes;
            pagingParameters = getpagingParameters();   //Gets from session variables
            if (ModelState.IsValid)
            {
                if (myViewModel.searchText != null && myViewModel.searchText.Trim().Length > 0)
                {
                    searchText = myViewModel.searchText.Trim();
                    mySet = mySet.Where(x => x.AuthorName.Contains(searchText)
                        || x.QuoteText.Contains(searchText)
                        );
                }
                if (myViewModel.searchCategoryId > 0)
                {
                    if (myViewModel.searchCategoryId > 0)
                    {
                        //33 is not defined.   Almost all records have the 3rd category as undefined. 
                        //This way new records can be isolated and updated with valid categories. 
                        if (myViewModel.searchCategoryId == 33)  
                        {
                            mySet = mySet.Where(x => x.CategoryId == myViewModel.searchCategoryId
                            );
                        }
                        else
                        {
                            mySet = mySet.Where(x => x.CategoryId == myViewModel.searchCategoryId
                            || x.CategoryId2 == myViewModel.searchCategoryId
                            || x.CategoryId3 == myViewModel.searchCategoryId
                            );
                        }
                    }
                }
                if (myViewModel.searchQuoteTypeId > 0)
                {
                    mySet = mySet.Where(x => x.QuoteTypeId == myViewModel.searchQuoteTypeId);
                }
            }
            // Set Paging parameters
            pagingParameters.totalItemCount = mySet.Count();
            if (Request.Form["Top"] != null)
            {
                pagingParameters.keyClicked = "TOP";
            }
            else if (Request.Form["NEXT"] != null)
            {
                pagingParameters.keyClicked = "NEXT";
            }
            else if (Request.Form["PREVIOUS"] != null)
            {
                pagingParameters.keyClicked = "PREVIOUS";
            }
            else if (Request.Form["BOTTOM"] != null)
            {
                pagingParameters.keyClicked = "BOTTOM";
            }
            if (Request.Form["SUBMITSEARCH"] != null)
            {
                pagingParameters.keyClicked = "SEARCH";
            }
            //previousStart is taken from session or from an initialized value of 0;
            ro = PagingUtility.getPageSettings(pagingParameters);
            if (!ro.IsValid)
            {
                ViewBag.ErrorMessages = ro.ErrorMessages;
                return View(myViewModel);
            }
            myViewModel.pagingParameters = pagingParameters;
            SetSession.SetPagingParameters(pagingParameters);
            mySet = mySet.Include(q => q.Category).Include(x => x.QuoteType).OrderByDescending(x => x.Likes);
            myViewModel.quotesList = mySet.Skip(pagingParameters.newStart).Take(pagingParameters.newCount).ToList();
            return View(myViewModel);
        }

        // GET: Quotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: Quotes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            ViewBag.quoteTypeId = new SelectList(db.QuoteType, "QuoteTypeId", "QuoteTypeDescription");
            EditQuoteViewModel newQuote = new EditQuoteViewModel();
            newQuote.AuthorName = "";
            newQuote.QuoteText = "";
            newQuote.Contributor = "";
            newQuote.CategoryId = 33;
            newQuote.CategoryId2 = 33;
            newQuote.CategoryId3 = 33;
            newQuote.CategoryDictionary = CategoryUtility.getCategoryDictionaryIntString();
            newQuote.CategoryArraylist = CategoryUtility.getCategoryArraylist();
            return View(newQuote);
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuoteId,QuoteText,AuthorName,Likes,Contributor,QuoteTypeId,CategoryId,CategoryId2,CategoryId3,CreatedByLogonAccountId,UpdatedByLogonAccountId,RatingTypeId,CreateDate,UpdateDate")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                if (QuotesParables.Utilities.FindDuplicates.duplicateExists(quote.QuoteText, 0))
                {
                    ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
                    ViewBag.quoteTypeId = new SelectList(db.QuoteType, "QuoteTypeId", "QuoteTypeDescription");
                    ViewBag.ErrorMessage = "Duplicate Quote Found - Record not inserted.";
                    EditQuoteViewModel newQuote = new EditQuoteViewModel();
                    newQuote.AuthorName = quote.AuthorName;
                    newQuote.QuoteText = quote.QuoteText;
                    newQuote.CategoryId = quote.CategoryId;
                    newQuote.CategoryId2 = quote.CategoryId2;
                    newQuote.CategoryId3 = quote.CategoryId3;
                    newQuote.CategoryDictionary = CategoryUtility.getCategoryDictionaryIntString();
                    newQuote.CategoryArraylist = CategoryUtility.getCategoryArraylist();
                    return View(newQuote);
                }
                else
                {
                    if (HttpContext.Session["CurrentUser"] != null)
                    {
                        quote.Aprroved = "Y";
                        quote.CreatedByLogonAccountId = getLogonUser().LogonAccountId;
                        quote.UpdatedByLogonAccountId = 1;
                    }
                    else
                    {
                        quote.Aprroved = "N";
                        quote.CreatedByLogonAccountId = 1009;
                        quote.UpdatedByLogonAccountId = 1009;
                    }
                    quote.CreateDate = DateTime.Now;
                    quote.UpdateDate = DateTime.Now;
                    quote.Likes = 0;
                    db.Quotes.Add(quote);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.Description), "CategoryId", "Description", quote.CategoryId);
            return View(quote);
        }

        // GET: Quotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            if (quote.AuthorName != null && quote.AuthorName.ToUpper() == "UNKNONWN")
            {
                quote.AuthorName = "";
            }
            EditQuoteViewModel editQuoteViewModel = new EditQuoteViewModel();
            editQuoteViewModel.QuoteId = quote.QuoteId;
            editQuoteViewModel.QuoteText = quote.QuoteText;
            editQuoteViewModel.AuthorName = quote.AuthorName;
            editQuoteViewModel.CategoryId = quote.CategoryId;
            editQuoteViewModel.CategoryId2 = quote.CategoryId2;
            editQuoteViewModel.CategoryId3 = quote.CategoryId3;
            editQuoteViewModel.QuoteTypeId = quote.QuoteTypeId;
            editQuoteViewModel.CreateDate = quote.CreateDate;
            editQuoteViewModel.UpdateDate = quote.UpdateDate;
            editQuoteViewModel.Likes = quote.Likes;
            editQuoteViewModel.Aprroved= quote.Aprroved;
            editQuoteViewModel.CreatedByLogonAccountId = quote.CreatedByLogonAccountId;
            editQuoteViewModel.UpdatedByLogonAccountId = quote.UpdatedByLogonAccountId;
            editQuoteViewModel.CategoryDictionary = CategoryUtility.getCategoryDictionaryIntString();
            editQuoteViewModel.CategoryArraylist = CategoryUtility.getCategoryArraylist();
            editQuoteViewModel.repeatUpdate = getRepeatUpdate();
            ViewBag.CategoryId = new SelectList(db.Categories.OrderBy(x => x.Description), "CategoryId", "Description", quote.CategoryId);
            ViewBag.quoteTypeId = new SelectList(db.QuoteType, "QuoteTypeId", "QuoteTypeDescription");
            return View(editQuoteViewModel);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Quotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuoteId,QuoteText,AuthorName,Likes,QuoteTypeId,CategoryId,CategoryId2,CategoryId3,CreatedByLogonAccountId,UpdatedByLogonAccountId,RatingTypeId,CreateDate,UpdateDate,repeatUpdate,Approved")] EditQuoteViewModel editQuoteViewModel)
        {
            if (ModelState.IsValid)
            {
                try {
                    Quote newQuote = new Quote();
                    newQuote.QuoteId = editQuoteViewModel.QuoteId;

                    int lengthBefore, lengthAfter;
                    lengthBefore = editQuoteViewModel.QuoteText.Length;
                    newQuote.QuoteText = editQuoteViewModel.QuoteText.TrimEnd();
                    lengthAfter = newQuote.QuoteText.Length;

                    newQuote.AuthorName = editQuoteViewModel.AuthorName;
                    newQuote.CategoryId = editQuoteViewModel.CategoryId;
                    newQuote.CategoryId2 = editQuoteViewModel.CategoryId2;
                    newQuote.CategoryId3 = editQuoteViewModel.CategoryId3;
                    newQuote.QuoteTypeId = editQuoteViewModel.QuoteTypeId;
                    newQuote.Likes = editQuoteViewModel.Likes;
                    newQuote.UpdateDate = DateTime.Now;
                    newQuote.UpdatedByLogonAccountId = getLogonUser().LogonAccountId;
                    newQuote.CreatedByLogonAccountId = editQuoteViewModel.CreatedByLogonAccountId;
                    newQuote.CreateDate = editQuoteViewModel.CreateDate;
                    db.Entry(newQuote).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    SetSession.SetRepeatUpdate(editQuoteViewModel.repeatUpdate);
                    if (editQuoteViewModel.repeatUpdate == "Y")
                    {
                        Quote nextQuote = db.Quotes.Where(x => x.UpdatedByLogonAccountId == 2).OrderBy(x => x.QuoteId).FirstOrDefault();
                        if (nextQuote != null)
                        {
                            int nextId = nextQuote.QuoteId;
                            //editQuoteViewModel.CategoryId = new SelectList(db.Categories.OrderBy(x => x.Description), "CategoryId", "Description");
                            //editQuoteViewModel.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
                            //editQuoteViewModel.quoteTypeId = new SelectList(db.QuoteType, "QuoteTypeId", "QuoteTypeDescription");
                            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
                            ViewBag.quoteTypeId = new SelectList(db.QuoteType, "QuoteTypeId", "QuoteTypeDescription");
                            return RedirectToAction("Edit", "Quotes", new { id = nextId });
                        }
                    }
                }
                catch (Exception exception)
                {
                    ArrayList errorList = new ArrayList();
                    errorList.Add(exception.Message);
                    if (exception.InnerException != null)
                    {
                        errorList.Add(exception.InnerException.InnerException.Message);
                    }
                    ViewBag.ErrorMessages = errorList;
                }
            }
            return RedirectToAction("Index", "Quotes");
        }

        // GET: Quotes/Like/5
        public ActionResult Like(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Quote quote = db.Quotes.Find(id);
                if (quote == null)
                {
                    return HttpNotFound();
                }
                quote.Likes++;
                db.Entry(quote).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Quotes");
            }
            catch (Exception exception)
            {
                ArrayList errorList = new ArrayList();
                errorList.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    errorList.Add(exception.InnerException.InnerException.Message);
                }
                ViewBag.ErrorMessages = errorList;
            }
            return RedirectToAction("Index", "Quotes");
        }

        // GET: Quotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quote quote = db.Quotes.Find(id);
            db.Quotes.Remove(quote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //----------------------------------------------
        //new methods
        //----------------------------------------------
        public ActionResult FindDuplicates()
        {
            InputQuotes myInputQuotes = new InputQuotes();
            myInputQuotes.QuotesDuplicates = "";
            return View(myInputQuotes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FindDuplicates([Bind(Include = "QuotesInput")] InputQuotes inputQuotes)
        {
            ReturnObject ro = new ReturnObject();
            if (ModelState.IsValid)
            {
                ro = QuotesParables.Utilities.FindDuplicates.listDuplicates();
                if (ro.IsValid)
                {
                    string returnValue = "No Records Found";
                    if (!(ro.ReturnObj1 == null || ((string)ro.ReturnObj1).Trim() == ""))
                    {
                        returnValue = (string)ro.ReturnObj1;
                    }

                    inputQuotes.QuotesDuplicates = returnValue;
                }
            }
            return View(inputQuotes);
        }

        public ActionResult InputQuotes()
        {
            InputQuotes myInputQuotes = new InputQuotes();
            myInputQuotes.QuotesInput = "Enter your list of quotes separated by '$$$'";
            return View(myInputQuotes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InputQuotes([Bind(Include = "QuotesInput")] InputQuotes inputQuotes)
        {
            ReturnObject ro = new ReturnObject();
            var errorList = new List<string>();
            ro = ParseUtil.parseAndInsert(inputQuotes.QuotesInput);
            inputQuotes = new InputQuotes(); 
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessages = ro.ErrorMessages;
                return View(inputQuotes);
            }
            inputQuotes.QuotesDuplicates = (string)ro.ReturnObj1;
            string s = string.Format("{0}", ro.ReturnObj2);
            errorList.Add(s + " records were inserted.");
            ViewBag.ErrorMessages = errorList; 
            return View(inputQuotes);
        }

        public ActionResult UpdateList()
        {
            var quotes = db.Quotes;
            return View(quotes.ToList());
        }
        public int getsearchCategoryId()
        {
            var searchCategoryId = HttpContext.Session["searchCategoryId"];
            if (searchCategoryId == null)
            {
                return 0;
            }
            else
            {
                return (int)searchCategoryId;
            }
        }

        public PagingParameters getpagingParameters()
        {
            PagingParameters pagingParameters = (PagingParameters) HttpContext.Session["pagingParameters"];
            if (pagingParameters == null)
            {
                pagingParameters = new PagingParameters();
                pagingParameters.previousStart = 0;
                pagingParameters.totalItemCount = 1;
                pagingParameters.itemsPerPage = 20;
                pagingParameters.keyClicked = "";
                pagingParameters.newStart = 1;
                pagingParameters.newCount = 1;
                pagingParameters.previousIsActives = true;
                pagingParameters.nextIsActives = true;
                pagingParameters.topIsActives = true;
                pagingParameters.bottomIsActives = true;
            }
            return pagingParameters;
        }

        public string getSessionSearchText()
        {
            var searchText = HttpContext.Session["searchText"];
            if (searchText == null)
            {
                return "";
            }
            else
            {
                return searchText.ToString();
            }
        }
        public int getSearchDataTypeId()
        {
            var searchDataTypeId = HttpContext.Session["searchDataTypeId"];
            if (searchDataTypeId == null)
            {
                return 0;
            }
            else
            {
                return (int)searchDataTypeId;
            }
        }
        public LogonAccount getLogonUser()
        {
            var logonUser = HttpContext.Session["CurrentUser"];
            if (logonUser == null)
            {
                return (LogonAccount)logonUser;
            }
            else
            {
                return (LogonAccount)logonUser;
            }
        }
        public string getRepeatUpdate()
        {
            var repeatUpdate = HttpContext.Session["repeatUpdate"];
            if (repeatUpdate == null)
            {
                return "N";
            }
            else
            {
                return repeatUpdate.ToString();
            }
        }
    }
}