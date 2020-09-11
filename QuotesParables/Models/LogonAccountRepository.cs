using System;
using System.Collections.Generic;
using System.Linq;
namespace QuotesParables.Models
{
    public class LogonAccountRepository
    {
        public IEnumerable<LogonAccount> GetList()
        {
            var context = new QuotesContext();

            return context.LogonAccounts.ToList();
        }

        public LogonAccount GetById(int id)
        {
            var context = new QuotesContext();
            return context.LogonAccounts.Find(id);
        }

        public LogonAccount GetByUserId(string uid)
        {
            var context = new QuotesContext();
            var currentUser = context.LogonAccounts.FirstOrDefault(x => x.EMail.ToUpper() == uid.ToUpper());
            return currentUser;
        }

        public bool GenerateNewLogonAccount()
        {
            try
            {
                QuotesContext context = new QuotesContext();
                var logonAccount = new LogonAccount();
                logonAccount.Name = "Stan Merrill";
                logonAccount.EMail = "stan@stanleymerrill.com";
                logonAccount.Password = "kwaben1a";
                logonAccount.SecurityLevel = "S";
                context.LogonAccounts.Add(logonAccount);
                context.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {
                var message = ex.Message;
                var ie = ex.InnerException;
                return false;
            }


        }

    }
}
