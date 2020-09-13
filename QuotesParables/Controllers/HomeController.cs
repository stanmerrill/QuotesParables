using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuotesParables.ViewModels;
using QuotesParables.Models;
using QuotesParables.Utilities;


namespace QuotesParables.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            /*
             * I am using my own security dtabase - see logonAccounts Controller
             */
            if (HttpContext.Session != null)
            {
                HttpContext.Session["CurrentUser"] = null;
            }
            //WebSecurity.Logout();
            return RedirectToAction("Login");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            LogonAccountRepository lar = new LogonAccountRepository();
            var errorList = new List<string>();
            try
            {
                {
                    var user = lar.GetByUserId(model.Email);
                    if (user != null)
                    {
                        string decryptedPassword = Encryptor.DecryptString(user.Password);
                        if (decryptedPassword == model.Password)
                        {
                            SetSession.SetSessionUser(user);
                            return RedirectToAction("Index", "Quotes");
                        }
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        return View(model);
                    }
                }
            }
            catch (Exception exception)
            {
                errorList.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    errorList.Add(exception.InnerException.Message);
                }
                ViewBag.ErrorMessages = errorList;
            }
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }
        // POST: Home/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "LogonAccountId,Name,EMail,Password,SecurityLevel")] LogonAccount logonAccount)
        {
            QuotesContext db = new QuotesContext();
            ReturnObject ro = new ReturnObject();
            if (ModelState.IsValid)
            {
                logonAccount.Password = Encryptor.EncryptString(logonAccount.Password);
                logonAccount.SecurityLevel = "3"; 
                db.LogonAccounts.Add(logonAccount);
                db.SaveChanges();
                ro = sendEmailToNewUser(logonAccount);
                if (!ro.IsValid)
                {
                    ViewBag.ErrorMessage = ro.ErrorMessages; 
                }
                return RedirectToAction("Index","Quotes");
            }
            return View(logonAccount);
        }
        public ReturnObject sendEmailToNewUser(LogonAccount logonAccount)
        {
            ReturnObject ro = new ReturnObject();
            var emailInput = new EmailInput();
            try
            {
                //--------------------------------------------------------------------------------------------------------
                //  Email to student
                //--------------------------------------------------------------------------------------------------------
                string enryptedID = Encryptor.EncryptString(logonAccount.LogonAccountId.ToString());
                emailInput.Body = "You must click on the link below to activate your account./r/n <a  href=\"MyDharmaQuotes.com/Homne/Activate/" + enryptedID + "\">Click Here4</a>";
                emailInput.Subject = "Activate your MyDharmaQuotes.Com account";
                emailInput.EmailToList.Add(logonAccount.EMail);
                var eu = new EmailUtility();
                ro = eu.SendEmail(emailInput);
                if (!ro.IsValid)
                {
                    return ro;
                }
            }
            catch (Exception ex)
            {
                ro.IsValid = false;
                ro.ErrorMessages.Add(ex.Message);
                if (ex.InnerException != null)
                {
                    ro.ErrorMessages.Add(ex.InnerException.Message);
                }
            }
            return ro;
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}