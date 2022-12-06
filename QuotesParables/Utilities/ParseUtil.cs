using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using QuotesParables.Models;


namespace QuotesParables.Utilities
{
    static public class ParseUtil
    {
        static public ReturnObject parseAndInsert(string input)
        {
            ReturnObject ro = new ReturnObject();
            QuotesRepository qr = new QuotesRepository();
            Quote newQuote;
            try
            {
                StringBuilder sbDuplicateRecords = new StringBuilder();
                sbDuplicateRecords.Append(" ");
                input = input.Replace("$$$", "^");
                string[] records = input.Split('^');
                int recordsInserted = 0;
                foreach (string quoteText in records)
                {
                    if (quoteText.Trim() != "")
                    {
                        if (FindDuplicates.duplicateExists(quoteText, 0))
                        {
                            sbDuplicateRecords.Append(quoteText);
                            sbDuplicateRecords.Append(System.Environment.NewLine);
                            ro.IsValid = false;
                        }
                        else
                        {
                            newQuote = new Quote();
                            if (quoteText.Length > 7500)
                            {
                            }
                            else
                            {
                                newQuote.QuoteText = quoteText;
                            }
                            newQuote.CategoryId = 33;
                            newQuote.CategoryId2 = 33;
                            newQuote.CategoryId3 = 33;
                            newQuote.QuoteTypeId = 1;
                            newQuote.AuthorName = "unknown";
                            newQuote.Likes = 0;
                            newQuote.CreateDate = DateTime.Now;
                            newQuote.UpdateDate = DateTime.Now;
                            newQuote.CreatedByLogonAccountId = 2;
                            newQuote.UpdatedByLogonAccountId = 2;
                            ro = qr.insertQuote(newQuote);
                            recordsInserted++;
                            if (!ro.IsValid)
                            {
                                return ro;
                            }
                        }
                    }
                }
                ro.ReturnObj1 = sbDuplicateRecords.ToString();
                ro.ReturnObj2 = recordsInserted;
                return ro;
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
            return ro;
        }

        static public ReturnObject StripQuoteTextOfLastBlankLine(string input)
        {
            ReturnObject ro = new ReturnObject();
            try
            {
                input = input.TrimEnd();
                ro.ReturnObj1 = input;
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
            return ro;
        }
        static public ReturnObject stripAllLinesOfDatabase(string input)
        {
            ReturnObject ro = new ReturnObject();
            QuotesRepository qr = new QuotesRepository();
            ro.ReturnObj1 = qr.getNextQuote();
            IEnumerable<Quote> quoteCollection = (IEnumerable<Quote>)ro.ReturnObj1;

            foreach (Quote quote in quoteCollection)
            {
                ro = StripQuoteTextOfLastBlankLine(quote.QuoteText);
                quote.QuoteText = (string)ro.ReturnObj1;
            }
            try
            {
                input = input.TrimEnd();
                ro.ReturnObj1 = input;
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
            return ro;
        }
        static public string createRawData(string input)
        {
            StringBuilder sb = new StringBuilder();
            string currentLine = input.Replace(Environment.NewLine, "~").TrimEnd();
            string outputLine = "";
            string[] lines = currentLine.Split('~');
            foreach (string line in lines)
            {
                if (line.Trim() != "")
                {
                    outputLine = "<p class=\"textBodyDiv h6\" \">" + line + "</p>";

                    sb.Append(outputLine);
                }
            }
            sb.Append(Environment.NewLine);
            string myQuoteText = sb.ToString();
            return myQuoteText;
        }
    }
}