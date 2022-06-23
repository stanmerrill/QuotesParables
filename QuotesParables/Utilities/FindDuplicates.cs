using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using QuotesParables.Models;
using System.Text;
using System.Web.Services.Protocols;

namespace QuotesParables.Utilities
{
    static public class FindDuplicates
    {
        static public ReturnObject listDuplicates()
        {
            ReturnObject ro = new ReturnObject();
            QuotesContext db = new QuotesContext();
            IEnumerable<Quote> quoteList = db.Quotes;
            ArrayList searchStringsArray = new ArrayList();
            ArrayList duplicateArray = new ArrayList();
            try
            {
                // Build a list of truncated quotes
                foreach (Quote quote in quoteList)
                {
                    SearchString searchString = new SearchString();
                    searchString.key = quote.QuoteId;
                    string truncatedQuote = quote.QuoteText;
                    if(truncatedQuote.Length > 35)
                    {
                        truncatedQuote = truncatedQuote.Substring(0, 35);
                    }
                    searchString.searchText = truncatedQuote;
                    searchStringsArray.Add(searchString);
                }
                int count;
                // Check for duplicates 
                foreach (SearchString searchString in searchStringsArray)
                {
                    IEnumerable<Quote> dupList =  db.Quotes.Where(x => x.QuoteText.Contains(searchString.searchText));
                    if (dupList != null && dupList.Count() >1 )
                    {
                        count = dupList.Count();
                        SearchString searchStringDup = new SearchString();
                        searchStringDup.searchText = searchString.searchText;
                        duplicateArray.Add(searchStringDup);
                    }
                }
                // print out duplicates 
                StringBuilder sb = new StringBuilder();
                foreach (SearchString searchString in duplicateArray)
                {
                    sb.Append(searchString.searchText);
                    sb.Append(System.Environment.NewLine);
                }
                ro.ReturnObj1 = sb.ToString();
                //printLine(sb.ToString());

            }
            catch (Exception exception)
            {
                ro.ErrorMessages.Add(exception.Message);
                if (exception.InnerException != null)
                {
                    ro.ErrorMessages.Add(exception.InnerException.InnerException.Message);
                }
                ro.errorMethodName = "listDuplicates";
                ro.IsValid = false;
            }
            return ro;
        }
        public static bool duplicateExists(string input, int maxFind)
        {
            QuotesContext db = new QuotesContext();
            string truncatedInput; 
            if(input.Length >= 40)
            {
                truncatedInput = input.Substring(0, 40);
            }
            else
            {
                truncatedInput = input; 
            }
            IEnumerable<Quote> dupList = db.Quotes.Where(x => x.QuoteText.Contains(truncatedInput));
            if (dupList != null && dupList.Count() > maxFind)
            {
                return true;
            }
            return false;
        }
        public static void printLine(string input)
        {
            string path = @"C:\Users\StanXPS\Google Drive\Documents\TextFiles\scaratchpad.txt";
            try
            {
                if (System.IO.File.Exists(path))
                {
                    //writes to file
                    System.IO.File.WriteAllText(path, input);
                }
                else
                {
                    // Create the file.
                    using (FileStream fs = File.Create(path))
                    {
                        System.IO.File.WriteAllText(path, input + "\n");
                    }

                }
                //// Open the stream and read it back.
                //using (StreamReader sr = File.OpenText(path))
                //{
                //    string s = "";
                //    while ((s = sr.ReadLine()) != null)
                //    {
                //        Console.WriteLine(s);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
    public class SearchString{
        public int key;
        public string searchText;
        public ArrayList duplicateKeys = new ArrayList();
    }
}