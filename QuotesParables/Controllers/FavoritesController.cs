using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuotesParables.Models;

namespace QuotesParables.Controllers
{
    public class FavoritesController : Controller
    {
        private QuotesContext db = new QuotesContext();

        // GET: Favorites
        public ActionResult Index()
        {
            var favorites = db.Favorites.Include(f => f.LogonAccount).Include(f => f.Quote);
            return View(favorites.ToList());
        }

        // GET: Favorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: Favorites/Create
        public ActionResult Create()
        {
            ViewBag.LogonAcountId = new SelectList(db.LogonAccounts, "LogonAccountId", "Name");
            ViewBag.QuoteId = new SelectList(db.Quotes, "QuoteId", "QuoteText");
            return View();
        }

        // POST: Favorites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuoteCategoryId,LogonAcountId,QuoteId")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Favorites.Add(favorite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LogonAcountId = new SelectList(db.LogonAccounts, "LogonAccountId", "Name", favorite.LogonAcountId);
            ViewBag.QuoteId = new SelectList(db.Quotes, "QuoteId", "QuoteText", favorite.QuoteId);
            return View(favorite);
        }

        // GET: Favorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            ViewBag.LogonAcountId = new SelectList(db.LogonAccounts, "LogonAccountId", "Name", favorite.LogonAcountId);
            ViewBag.QuoteId = new SelectList(db.Quotes, "QuoteId", "QuoteText", favorite.QuoteId);
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuoteCategoryId,LogonAcountId,QuoteId")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LogonAcountId = new SelectList(db.LogonAccounts, "LogonAccountId", "Name", favorite.LogonAcountId);
            ViewBag.QuoteId = new SelectList(db.Quotes, "QuoteId", "QuoteText", favorite.QuoteId);
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
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
    }
}
