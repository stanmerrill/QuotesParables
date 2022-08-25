using System.Collections.Generic;
using QuotesParables.Models;
using QuotesParables.Utilities;
using System.Data.Entity;
using System.Text;

namespace QuotesParables.ViewModels
{
    public class QuoteListViewModel
    {
        public List<Quote> quotesList { get; set; }
        public List<Category>  CategoryDropDown { get; set; }
        public List<QuoteType> TypeDropDown { get; set; }
        public int searchCategoryId { get; set; }
        public int searchQuoteTypeId { get; set; }
        public string searchText { get; set; }
        public int unapprovedCount { get; set; }
        public PagingParameters pagingParameters { get; set;  }
        public string getCategoryText(int catId1, int catId2, int catId3)
        {
            StringBuilder sb = new StringBuilder();
            if (catId1 != 33)
            {
                sb.Append("--");
                sb.Append(CategoryUtility.getDeseription(catId1));
            }
            if (catId2 != 33)
            {
                sb.Append("&nbsp;&nbsp;&nbsp;--");
                sb.Append(CategoryUtility.getDeseription(catId2));
            }
            if (catId3 != 33)
            {
                sb.Append("&nbsp;&nbsp;&nbsp;--");
                sb.Append(CategoryUtility.getDeseription(catId3));
            }

            return sb.ToString();
        }

    }
}