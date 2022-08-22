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
    public class QuoteTypesController : Controller
    {
        private QuotesContext db = new QuotesContext();

        // GET: QuoteTypes
        public ActionResult Index()
        {
            return View(db.QuoteType.ToList());
        }

        // GET: QuoteTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuoteType quoteType = db.QuoteType.Find(id);
            if (quoteType == null)
            {
                return HttpNotFound();
            }
            return View(quoteType);
        }

        // GET: QuoteTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuoteTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuoteTypeId,QuoteTypeDescription")] QuoteType quoteType)
        {
            if (ModelState.IsValid)
            {
                db.QuoteType.Add(quoteType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quoteType);
        }

        // GET: QuoteTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuoteType quoteType = db.QuoteType.Find(id);
            if (quoteType == null)
            {
                return HttpNotFound();
            }
            return View(quoteType);
        }

        // POST: QuoteTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuoteTypeId,QuoteTypeDescription")] QuoteType quoteType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quoteType).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quoteType);
        }

        // GET: QuoteTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuoteType quoteType = db.QuoteType.Find(id);
            if (quoteType == null)
            {
                return HttpNotFound();
            }
            return View(quoteType);
        }

        // POST: QuoteTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuoteType quoteType = db.QuoteType.Find(id);
            db.QuoteType.Remove(quoteType);
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
