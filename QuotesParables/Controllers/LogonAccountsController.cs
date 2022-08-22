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
    public class LogonAccountsController : Controller
    {
        private QuotesContext db = new QuotesContext();

        // GET: LogonAccounts
        public ActionResult Index()
        {
            return View(db.LogonAccounts.ToList());
        }

        // GET: LogonAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogonAccount logonAccount = db.LogonAccounts.Find(id);
            if (logonAccount == null)
            {
                return HttpNotFound();
            }
            return View(logonAccount);
        }

        // GET: LogonAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogonAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogonAccountId,Name,EMail,Password,SecurityLevel")] LogonAccount logonAccount)
        {
            if (ModelState.IsValid)
            {
                logonAccount.Password = QuotesParables.Utilities.Encryptor.EncryptString(logonAccount.Password);
                db.LogonAccounts.Add(logonAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logonAccount);
        }

        // GET: LogonAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogonAccount logonAccount = db.LogonAccounts.Find(id);
            if (logonAccount == null)
            {
                return HttpNotFound();
            }
            return View(logonAccount);
        }

        // POST: LogonAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogonAccountId,Name,EMail,Password,SecurityLevel")] LogonAccount logonAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logonAccount).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logonAccount);
        }

        // GET: LogonAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogonAccount logonAccount = db.LogonAccounts.Find(id);
            if (logonAccount == null)
            {
                return HttpNotFound();
            }
            return View(logonAccount);
        }

        // POST: LogonAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogonAccount logonAccount = db.LogonAccounts.Find(id);
            db.LogonAccounts.Remove(logonAccount);
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
