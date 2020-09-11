using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;

namespace QuotesParables.Utilities
{
    public class EmailUtility
    {
        public ReturnObject SendEmail(EmailInput eiInput)
        {
            ReturnObject ro = new ReturnObject();
            SmtpClient SmtpServer;
            MailMessage mail = new MailMessage();
            var mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            try
            {
                foreach (string emailAddress in eiInput.EmailToList)
                {
                    mail.To.Add(emailAddress);
                }
                mail.Subject = eiInput.Subject;
                mail.Body = eiInput.Body;
                mail.IsBodyHtml = true;
                if (!IsLocal())
                {
                    //----------------
                    // SMARTERASP - MyDharmaQuotes.com 
                    //---------------------------
                    mail.From = new MailAddress("postmaster@MyDharmaQuotes.com");
                    NetworkCredential credentials = new NetworkCredential("postmaster@MyDharmaQuotes.com", "kwaben1a");
                    SmtpServer = new SmtpClient("mail.MyDharmaQuotes.com");
                    SmtpServer.Credentials = credentials;
                    //____________________________________________________
                    //  From IMH reg settings below
                    //____________________________________________________
                    //mail.From = new MailAddress("postmaster@imhreg.com");
                    //NetworkCredential credentials = new NetworkCredential("postmaster@imhreg.com", "Comp1492assion$");
                    //SmtpServer = new SmtpClient("mail.imhreg.com");
                    //SmtpServer.Credentials = credentials;
                    //-----------------------------
                    //Arvixe Parameters  
                    //-----------------------------
                    //mail.From = new MailAddress("support@mns-tage.com");
                    //SmtpServer = new SmtpClient("mail.mns-tage.com", 25);
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("support@mns-tage.com", "bessI6249e$");
                    //SmtpServer.UseDefaultCredentials = false;
                    //SmtpServer.Timeout = 100000;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("support@mns-tage.com", "bessI6249e$");
                }
                else
                {
                    //--------------------------------------------------------------------------------------------
                    // The following settings worked!!!
                    //--------------------------------------------------------------------------------------------
                    //mail.From = new MailAddress("stanbus@yahoo.com");
                    //mail.IsBodyHtml = true;
                    //SmtpServer = new SmtpClient("smtp.gmail.com");
                    //SmtpServer.Port = 587;
                    //SmtpServer.EnableSsl = true;
                    //SmtpServer.UseDefaultCredentials = false;
                    //SmtpServer.Timeout = 100000;
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("stanmerrill@gmail.com", "mcys vsol bttl cwep");
                    //--------------------------------------------------------------------------------------------
                    //Util.WriteLog("Local");
                    mail.From = new MailAddress("stanmbus@yahoo.com");
                    SmtpServer = new SmtpClient("smtp.yahoo.com");
                    //SmtpServer = new SmtpClient("smtp.gmail.com");
                    SmtpServer.Port = 587;
                    SmtpServer.EnableSsl = true;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Timeout = 100000;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("stanmbus@yahoo.com", "Loy1602ve$");
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("stanmerrill@gmail.com", "Log1602ve$");
                    //SmtpServer.Credentials = new System.Net.NetworkCredential("stanmerrill@gmail.com", "auhp lotg dgov ymhj");
                }
                SmtpServer.Send(mail);
                return ro; 
            }
            catch (Exception ex)
            {
                ro.IsValid = false;
                ro.ErrorMessages.Add(ex.Message);
                ro.ErrorMessages.Add("Path = " + mapPath);
                if (ex.InnerException != null)
                {
                    ro.ErrorMessages.Add(ex.InnerException.Message);
                }
            }
            return ro;
        }
        public bool IsLocal()
        {
            var mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            //Util.WriteLog(String.Format("Map Path is {0}", mapPath));
            if (mapPath != null && mapPath.StartsWith(@"C:\"))
            {
                //Util.WriteLog("returned false");
                return true;
            }
            return false;
        }
    }

    public class EmailInput     
    {
        public List<string> EmailToList = new List<string>();
        public string Subject = "";
        public string Body = "";
        public string From = "";
    }

    public class EmailValidate
    {
        static bool invalid = false;
        static public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        static private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }    
    }

}