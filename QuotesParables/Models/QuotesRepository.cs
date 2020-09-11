using System;
using System.Collections.Generic;
using System.Linq;
using QuotesParables.Utilities;
using System.Data;
using System.Data.Entity;

namespace QuotesParables.Models
{
    public class QuotesRepository
    {
        QuotesContext quotesContext = new QuotesContext();
        ReturnObject ro = new ReturnObject();
        public ReturnObject insertQuote(Quote myQuote)
        {
            try
            {
                quotesContext.Quotes.Add(myQuote);
                quotesContext.SaveChanges();
            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "insertQuote";
                ro.IsValid = false;
            }
            quotesContext.SaveChanges();
            quotesContext.Dispose();
            return ro;
        }
        public ReturnObject updateQuote(Quote myQuote)
        {
            try
            {
                quotesContext.Entry(myQuote).State = EntityState.Modified;
                quotesContext.SaveChanges();
            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "insertQuote";
                ro.IsValid = false;
            }
            quotesContext.SaveChanges();
            quotesContext.Dispose();
            return ro;
        }
        public ReturnObject getNextQuote()
        {
            try
            {
               Quote myQuote = quotesContext.Quotes.Where(x => x.UpdatedByLogonAccountId == 2).OrderBy(x => x.QuoteId).FirstOrDefault();
               if ( myQuote == null)
                {
                    ro.IsValid = false;
                    ro.ErrorMessages.Add("No record found");
                    return ro;
                }
                ro.ReturnObj1 = myQuote;
                return ro; 
            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "getNextQuote";
                ro.IsValid = false;
            }
            quotesContext.Dispose();
            return ro;
        }
        public ReturnObject getQuoteById(int quoteId)
        {
            try
            {
                Quote myQuote = quotesContext.Quotes.Where(x => x.QuoteId == quoteId).FirstOrDefault();
                if (myQuote == null)
                {
                    ro.IsValid = false;
                    ro.ErrorMessages.Add("No record found");
                    return ro;
                }
                ro.ReturnObj1 = myQuote;
                return ro;
            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "getNextQuote";
                ro.IsValid = false;
            }
            quotesContext.Dispose();
            return ro;
        }
        public ReturnObject getAllQuotes()
        {
            try
            {
                IEnumerable<Quote>  allQuotes = quotesContext.Quotes;
                if (allQuotes == null)
                {
                    ro.IsValid = false;
                    ro.ErrorMessages.Add("No record found");
                    return ro;
                }
                ro.ReturnObj1 = allQuotes;
                return ro;
            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "getNextQuote";
                ro.IsValid = false;
            }
            quotesContext.Dispose();
            return ro;
        }

    }
}