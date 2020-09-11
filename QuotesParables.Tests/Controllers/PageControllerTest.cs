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
    public class PageControllerTest
    {
        [TestMethod]
        public void intialLoad()
        {
            // Arrange
            PagingParameters pp = new PagingParameters();
            pp.previousStart = 0;
            pp.totalItemCount = 10;
            pp.itemsPerPage = 3;
            pp.keyClicked = "";
            pp.newStart = 1;
            pp.newCount = 1;
            pp.previousIsActives = false;
            pp.nextIsActives = false;
            pp.topIsActives = false;
            pp.bottomIsActives = false;
            // Act
            ReturnObject ro = PagingUtility.getPageSettings(pp);
            // Assert
            PagingParameters ppout = (PagingParameters)ro.ReturnObj1;
            Assert.IsTrue(pp.newStart == 0);
            Assert.IsTrue(pp.newCount == 3);
            Assert.IsTrue(pp.previousIsActives == false);
            Assert.IsTrue(pp.topIsActives == false);
            Assert.IsTrue(pp.nextIsActives == true);
            Assert.IsTrue(pp.bottomIsActives == true);
        }
        [TestMethod]
        public void pageNext()
        {
            // Arrange
            PagingParameters pp = new PagingParameters();
            pp.previousStart = 0;
            pp.totalItemCount = 10;
            pp.itemsPerPage = 3;
            pp.keyClicked = "NEXT";
            pp.newStart = 1;
            pp.newCount = 1;
            pp.previousIsActives = false;
            pp.nextIsActives = false;
            pp.topIsActives = false;
            pp.bottomIsActives = false;
            // Act
            ReturnObject ro = PagingUtility.getPageSettings(pp);
            // Assert
            PagingParameters ppout = (PagingParameters)ro.ReturnObj1;
            Assert.IsTrue(pp.newStart == 3);
            Assert.IsTrue(pp.newCount == 3);
            Assert.IsTrue(pp.previousIsActives == true);
            Assert.IsTrue(pp.nextIsActives == true)
;            Assert.IsTrue(pp.topIsActives == true);
            Assert.IsTrue(pp.bottomIsActives == true);
        }
        [TestMethod]
        public void pagePrevious()
        {
            // Arrange
            PagingParameters pp = new PagingParameters();
            pp.previousStart = 3;
            pp.totalItemCount = 10;
            pp.itemsPerPage = 3;
            pp.keyClicked = "PREVIOUS";
            pp.newStart = 0;
            pp.newCount = 3;
            pp.previousIsActives = false;
            pp.nextIsActives = false;
            pp.topIsActives = false;
            pp.bottomIsActives = false;
            // Act
            ReturnObject ro = PagingUtility.getPageSettings(pp);
            // Assert
            PagingParameters ppout = (PagingParameters)ro.ReturnObj1;
            Assert.IsTrue(pp.newStart == 0);
            Assert.IsTrue(pp.newCount == 3);
            Assert.IsTrue(pp.previousIsActives == false);
            Assert.IsTrue(pp.topIsActives == false);
            Assert.IsTrue(pp.nextIsActives == true);
            Assert.IsTrue(pp.bottomIsActives == true);
        }
    }
}

