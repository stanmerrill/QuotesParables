using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuotesParables;
using QuotesParables.Controllers;
using QuotesParables.Models;
using QuotesParables.Utilities;

namespace QuotesParables.Tests.Controllers
{
    [TestClass]
    public class QuotesControllerTest
    {
        [TestMethod]
        public void replaceEmbeddedPoundSign()
        {
            // Arrange
            // Act
            ReturnObject ro = new ReturnObject();
            ro = ReplaceCategoryembed.removeCategoriesFromQuoteText();
            // Assert
            Assert.IsNotNull(ro);
            Assert.IsTrue(ro.IsValid);
        }
        [TestMethod]
        public void setCategory2And3()
        {
            // Arrange
            // Act
            ///ViewResult result = controller.Index() as ViewResult;
            ReturnObject ro = new ReturnObject();
            ro = ReplaceCategoryembed.ReplaceCategories();
            // Assert
            Assert.IsNotNull(ro);
            Assert.IsTrue(ro.IsValid);
        }

        [TestMethod]
        public void findDuplicates()
        {
            // Arrange
            // Act
            ///ViewResult result = controller.Index() as ViewResult;
            ReturnObject ro = new ReturnObject();
            ro = FindDuplicates.listDuplicates();
            // Assert
            Assert.IsNotNull(ro);
            Assert.IsTrue(ro.IsValid);
        }
        [TestMethod]
        public void Parse()
        {
            // Arrange
            String inputString = "<p>A real love letter is made of insight, understanding, and compassion. Otherwise it’s not a love letter. A true love letter can produce a transformation in the other person, and therefore in the world. But before it produces a transformation in the other person, it has to produce a transformation within us. Some letters may take the whole of our lifetime to write.   - Thích Nhat Hanh<p>Suppose you are facing a difficult situation.  You don't know what to do.  The best thing to do is to sit in a meditative posture until you can accept things as they are.    Suzuki Roshi<p>Marcel Proust The real voyage of discovery consists not in seeing new sights, but in looking with new eyes.  (internet)<p>Rumi – “I felt in need of a great pilgrimage, so I sat still for three days”<p>Dogan - Meditation is not a way to enlightenment, Nor is it a method of achieving anything at all. It is peace itself. It is the actualization of wisdom, The ultimate truth of the oneness of all things.";
            // Act
            ///ViewResult result = controller.Index() as ViewResult;
            ReturnObject ro = new ReturnObject();
            ro = ParseUtil.parseAndInsert(inputString);
            // Assert
            Assert.IsNotNull(ro);
            Assert.IsTrue(ro.IsValid);
        }
        [TestMethod]
        public void stripOneRecord()
        {
            // Arrange
            QuotesRepository qr = new QuotesRepository();
            ReturnObject ro = qr.getQuoteById(626);
            Quote quote = (Quote) ro.ReturnObj1;
            int lengthOfQuote = quote.QuoteText.Length; 
            // Act
            
            ///ViewResult result = controller.Index() as ViewResult;
            ro = ParseUtil.StripQuoteTextOfLastBlankLine(quote.QuoteText);
            string returnString = (string)ro.ReturnObj1;
            int returnStringLenght = returnString.Length; 
            // Assert
            Assert.IsNotNull(ro);
            Assert.IsTrue(ro.IsValid);
            Assert.AreNotEqual(returnString.Length, lengthOfQuote);
        }
        [TestMethod]
        public void stripAllRecords()
        {
            // Arrange
            QuotesRepository qr = new QuotesRepository();
            ReturnObject ro = qr.getAllQuotes();
            if (ro.IsValid)
            {
                IEnumerable<Quote> quotesList = (IEnumerable<Quote>)ro.ReturnObj1;
                foreach (Quote quote in quotesList)
                {
                    ro = ParseUtil.StripQuoteTextOfLastBlankLine(quote.QuoteText);
                    quote.QuoteText = (string)ro.ReturnObj1;
                    qr.updateQuote(quote);
                }
            }
            // Assert
            Assert.IsNotNull(ro);
            Assert.IsTrue(ro.IsValid);
        }
    }
}
