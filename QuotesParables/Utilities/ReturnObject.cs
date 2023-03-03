using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotesParables.Utilities
{
    public class ReturnObject
    {
        public List<string> ErrorMessages = new List<string>();
        public string returnString = "";
        public Object ReturnObj1 = null;
        public Object ReturnObj2 = null;
        public Object ReturnObj3 = null;
        public bool IsValid = true;
        public string errorMethodName = "";
    }
}