using System;
using System.Collections.Generic;
using QuotesParables.Models;
using System.Data.Entity;
using System.Collections;
using System.Text;
using System.IO;

namespace QuotesParables.Utilities
{
    static public class ReplaceCategoryembed
    {
        static public ReturnObject removeCategoriesFromQuoteText()
        {
            ReturnObject ro = new ReturnObject();
            try
            {
                ArrayList updateableQuotes = new ArrayList();
                using (var context = new QuotesContext())
                {
                    IEnumerable<Quote> quoteList = context.Quotes;
                    foreach (Quote quote in quoteList)
                    {
                        if (quote.QuoteText.Contains("###"))
                        {
                            updateableQuotes.Add(quote);
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                using (var context = new QuotesContext())
                {
                    foreach (Quote quote in updateableQuotes)
                    {
                        if (quote.QuoteText.Contains("###"))
                        {
                            string[] splitString = quote.QuoteText.Split('#');
                            quote.QuoteText = splitString[0].TrimEnd();

                            context.Entry(quote).State = EntityState.Modified;
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "removeCategoriesFromQuoteText";
                ro.IsValid = false;
            }
            return ro;
        }
        static public ReturnObject ReplaceCategories()
        {
            ReturnObject ro = new ReturnObject();
            try
            {
                ArrayList updateableQuotes = new ArrayList();
                using (var context = new QuotesContext())
                {
                    IEnumerable<Quote> quoteList = context.Quotes;
                    foreach (Quote quote in quoteList)
                    {
                        if (quote.QuoteText.Contains("###"))
                        {
                            updateableQuotes.Add(quote);
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                using (var context = new QuotesContext())
                {
                    foreach (Quote quote in updateableQuotes)
                    {
                        if (quote.QuoteText.Contains("###"))
                        {
                            ro = updateQuoteWithCategoryIds(quote, context);
                            if (ro.IsValid)
                            {
                                Quote updatedQuote = (Quote)ro.ReturnObj1;
                                context.Entry(updatedQuote).State = EntityState.Modified;
                                context.SaveChanges();
                            }
                            else
                            {
                                sb.Append(quote.QuoteText);
                                sb.Append("\n");
                            }
                        }
                    }
                }
                string path = @"C:\Users\StanXPS\Google Drive\Documents\TextFiles\scaratchpad.txt";
                if (System.IO.File.Exists(path))
                {
                    //writes to file
                    System.IO.File.WriteAllText(path, sb.ToString());
                }
                else
                {
                    // Create the file.
                    using (FileStream fs = File.Create(path))
                    {
                        System.IO.File.WriteAllText(path, sb.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "ReplaceCategories";
                ro.IsValid = false;
            }
            return ro;
        }

        private static ReturnObject updateQuoteWithCategoryIds(Quote myQuote, QuotesContext context)
        {
            ReturnObject ro = new ReturnObject();
            string currentQuote;
            try
            {
                //------------------------------------------------------
                //   Example my qoute ###Cat2 ###Cat3 
                //------------------------------------------------------
                // element 0 is the quote - My Quote
                //         1 is the category - Cat2
                //         2 might be a second category - Cat 3
                //------------------------------------------------------
                //IDictionary<string, int> catDictionary = CategoryUtility.getCategoryDictionaryStringInt();
                currentQuote = myQuote.QuoteText;
                currentQuote = currentQuote.Replace("###insight", "###Insight");
                currentQuote = currentQuote.Replace("###Personal Responsibility", "###Responsibility");
                currentQuote = currentQuote.Replace("###Wisdom", "###Insight");
                currentQuote = currentQuote.Replace("###/", "###");
                currentQuote = currentQuote.Replace("###Mindfulness", "###Meditation");
                currentQuote = currentQuote.Replace("###", "#");
                string[] elements = myQuote.QuoteText.Split('#');
                int countNonBlank = 0;
                foreach (string item in elements)
                {
                    if (item.Trim() != "")
                    {
                        countNonBlank++;
                        if (countNonBlank == 2)
                        {
                            myQuote.CategoryId2 = CategoryUtility.getId(item.Trim());
                        }
                        if (countNonBlank == 3)
                        {
                            myQuote.CategoryId3 = CategoryUtility.getId(item.Trim());
                        }
                    }
                }
                ro.ReturnObj1 = myQuote;
            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "updateQuoteWithCategoryIds";
                ro.IsValid = false;
            }
            return ro;
        }
    }
}