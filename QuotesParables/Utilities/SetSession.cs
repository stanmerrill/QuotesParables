using System.Web;
using QuotesParables.Models;
using System.Collections.Generic;


namespace QuotesParables.Utilities
{
    public class SetSession
    {
        //public static void getCurrentSession (CurrenttSession currentSession)
        //{
        //    HttpContext.Current.Session.Add("currentSession", currentSession);
        //}
        public static void SetSessionUser(LogonAccount user)
        {
            HttpContext.Current.Session.Add("CurrentUser", user);
        }
        public static void SetSessionSearchText(string searchText)
        {
            HttpContext.Current.Session.Add("searchText", searchText);
        }
        public static void SetSessionSearchCategoryID(int searchCategoryId)
        {
            HttpContext.Current.Session.Add("searchCategoryId", searchCategoryId);
        }
        public static void SetSessionSearchQuoteTypeID(int quoteTypeId)
        {
            HttpContext.Current.Session.Add("searchQuoteTypeId", quoteTypeId);
        }
        public static void SetRepeatUpdate(string repeatUpdate)
        {
            HttpContext.Current.Session.Add("repeatUpdate", repeatUpdate);
        }
        public static void SetCategoryDictionary(IDictionary<int, string> myDictionary)
        {
            HttpContext.Current.Session.Add("categoryLookup", myDictionary);
        }
        public static void SetPagingParameters (PagingParameters pagingParameters)
        {
            HttpContext.Current.Session.Add("pagingParameters", pagingParameters);
        }

    }
    //public class CurrenttSession
    //{
    //    public static string searchText;
    //    public static string repeatUpdate;
    //    public static int searchCategoryId;
    //    public static int searchQuoteTypeId;
    //    public static int CategoryDropDown;
    //    public static int TypeDropDown;
    //    public static LogonAccount currentUser;
    //    public static IDictionary<int, string> myDictionary;
    //    public static PagingParameters pagingParameters;
    //}



}