using Microsoft.Ajax.Utilities;
using QuotesParables.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace QuotesParables.Utilities
{
    static public class HTMLUtility
    {
        public static ReturnObject getCategorySelectBoxStringFromArrayListAndSBParms(SelectBoxParameters selectBoxParameters, ArrayList arrayList, out string returnString)
        {
            string methodName = "getCategorySelectBoxStringFromArrayListAndSBParms";
            string selectedText = "";
            ReturnObject returnObject = new ReturnObject();
            StringBuilder stringBuilder = new StringBuilder();
            string selected = "";
            returnString = "";
            try
            {
                stringBuilder.AppendFormat(@"        <label class=""text-primary col-md-2"" for=""{0}"">{1}</label>", selectBoxParameters.fieldName, selectBoxParameters.fieldDisplayName);
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(@"        <div class=""col-md-10"">"); stringBuilder.Append(Environment.NewLine);
                stringBuilder.AppendFormat(@"            <select class=""form-control"" id=""{0}"" name=""{0}"">", selectBoxParameters.fieldName);
                stringBuilder.Append(Environment.NewLine);
                foreach (Category category in arrayList)
                {
                    selected = "";
                    if (selectBoxParameters.currentValue == category.CategoryId.ToString())
                    {
                        selected = "SELECTED";
                    }
                    stringBuilder.AppendFormat(@"<option value=""{0}"" {1} >{2}</option>", category.CategoryId, selected, category.Description);
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(@"</select>"); stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(@"</div>"); stringBuilder.Append(Environment.NewLine);
                stringBuilder.Replace(@"^%$", "}");
                stringBuilder.Replace(@"$%^", "{");
                returnString = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                returnObject.IsValid = false;
                //returnObject.ErrorMessages.Add(Utilities.FileIO.formatExceptionError(ex, className, methodName, ""));
            }
            return returnObject;
        }
        public static ReturnObject getTypeSelectBoxStringFromArrayListAndSBParms(SelectBoxParameters selectBoxParameters, ArrayList arrayList, out string returnString)
        {
            string methodName = "getTypeSelectBoxStringFromArrayListAndSBParms";
            string selectedText = "";
            ReturnObject returnObject = new ReturnObject();
            StringBuilder stringBuilder = new StringBuilder();
            string selected = "";
            returnString = "";
            try
            {
                stringBuilder.AppendFormat(@"        <label class=""text-primary col-md-2"" for=""{0}"">{1}</label>", selectBoxParameters.fieldName, selectBoxParameters.fieldDisplayName);
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(@"        <div class=""col-md-10"">"); stringBuilder.Append(Environment.NewLine);
                stringBuilder.AppendFormat(@"            <select class=""form-control"" id=""{0}"" name=""{0}"">", selectBoxParameters.fieldName);
                stringBuilder.Append(Environment.NewLine);
                foreach (QuoteType quoteType in arrayList)
                {
                    selected = "";
                    if (selectBoxParameters.currentValue == quoteType.QuoteTypeId.ToString())
                    {
                        selected = "SELECTED";
                    }
                    stringBuilder.AppendFormat(@"<option value=""{0}"" {1} >{2}</option>", quoteType.QuoteTypeId, selected, quoteType.QuoteTypeDescription);
                    stringBuilder.Append(Environment.NewLine);
                }

                stringBuilder.Append(@"</select>"); stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(@"</div>"); stringBuilder.Append(Environment.NewLine);
                stringBuilder.Replace(@"^%$", "}");
                stringBuilder.Replace(@"$%^", "{");
                returnString = stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                returnObject.IsValid = false;
                //returnObject.ErrorMessages.Add(Utilities.FileIO.formatExceptionError(ex, className, methodName, ""));
            }
            return returnObject;
        }
    }
    public class SelectBoxParameters
    {
        public string fieldName = "";
        public string fieldDisplayName = "";
        public string luTableName = "";
        public string luTableKeyFieldName = "";
        public string luTableDisplayFieldName = "";
        public string whereClause = "";
        public string currentValue = "";
        public string currentComparisonDropDown = "=";
        public string cssClass = "form-control";
        public bool hasAllOption = false;
    }

}