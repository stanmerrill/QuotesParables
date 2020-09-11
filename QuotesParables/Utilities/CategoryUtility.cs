using QuotesParables.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace QuotesParables.Utilities
{
    static public class CategoryUtility
    {
        static IDictionary<int, string> _categoryDictionaryIntString;
        static IDictionary<string, int> _categoryDictionaryStringInt;
        static ArrayList _categroyArrayList = new ArrayList();
        static public void refreshCategories()
        {
            _categoryDictionaryIntString = null;
            _categoryDictionaryStringInt = null;
            _categroyArrayList = new ArrayList();
        }
        static public string getDeseription(int categoryId)
        {
            return getCategoryDictionaryIntString()[categoryId];
        }
        static public int getId(string description)
        {
            return getCategoryDictionaryStringInt()[description];
        }
        static public ArrayList getCategoryArraylist()
        {
            using (var context = new QuotesContext())
            {
                if (_categroyArrayList != null && _categroyArrayList.Count >0 )
                {
                    return _categroyArrayList;
                }
                IEnumerable<Category> catArray = context.Categories.OrderBy(x => x.Description);
                foreach (Category cat in catArray)
                {
                    _categroyArrayList.Add(cat);
                }
                return _categroyArrayList;
            }
        }
        static public  IDictionary<int, string> getCategoryDictionaryIntString()
        {
            using (var context = new QuotesContext())
            {
                if (_categoryDictionaryIntString != null && _categoryDictionaryIntString.Count() > 0 )
                {
                    return _categoryDictionaryIntString;
                }
                _categoryDictionaryIntString = new Dictionary<int, string>();
                IEnumerable<Category> catArray = context.Categories.OrderBy(x => x.Description);
                foreach (Category cat in catArray)
                {
                    _categoryDictionaryIntString.Add(cat.CategoryId, cat.Description);
                }
                return _categoryDictionaryIntString;
            }
        }
        static private IDictionary<string,int> getCategoryDictionaryStringInt()
        {
            ReturnObject ro = new ReturnObject();
            try
            {
                using (var context = new QuotesContext())
                {
                    if (_categoryDictionaryStringInt != null && _categoryDictionaryStringInt.Count > 0)
                    {
                        return _categoryDictionaryStringInt;
                    }
                    _categoryDictionaryStringInt = new Dictionary<string, int>();
                    QuotesContext qr = new QuotesContext();
                    IEnumerable<Category> catArray = qr.Categories;
                    int catArrayCount = catArray.Count();
                    foreach (Category cat in catArray)
                    {
                        _categoryDictionaryStringInt.Add(cat.Description, cat.CategoryId);
                    }
                    return _categoryDictionaryStringInt;
                }
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
            return _categoryDictionaryStringInt;
        }
    }
}