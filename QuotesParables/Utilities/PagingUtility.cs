using System;

namespace QuotesParables.Utilities
{
    static public class PagingUtility
    {
        static public ReturnObject getPageSettings(PagingParameters pagingParameters)
        {
            ReturnObject ro = new ReturnObject();
            try
            {
                // --------------------------------------------------------------
                // set new start without checking boundaries
                // --------------------------------------------------------------
                pagingParameters.newStart = pagingParameters.previousStart;
                if (pagingParameters.keyClicked == "NEXT")
                {
                    pagingParameters.newStart = pagingParameters.previousStart + pagingParameters.itemsPerPage;
                }
                else if (pagingParameters.keyClicked == "PREVIOUS")
                {
                    pagingParameters.newStart = pagingParameters.previousStart - pagingParameters.itemsPerPage;
                }
                else if (pagingParameters.keyClicked == "BOTTOM")
                {
                    pagingParameters.newStart = pagingParameters.totalItemCount - pagingParameters.itemsPerPage;
                }
                else if (pagingParameters.keyClicked == "TOP")
                {
                    pagingParameters.newStart = 0;
                }
                else if (pagingParameters.keyClicked == "SEARCH")
                {
                    pagingParameters.newStart = 0;
                }
                // --------------------------------------------------------------
                // Correct newStart if necessary
                // --------------------------------------------------------------
                if (pagingParameters.newStart > pagingParameters.totalItemCount) 
                    { pagingParameters.newStart = pagingParameters.totalItemCount - pagingParameters.itemsPerPage; }
                if (pagingParameters.newStart < 0) 
                    { pagingParameters.newStart = 0; }
                // --------------------------------------------------------------
                // Correct newCount if necessary
                // --------------------------------------------------------------
                int newCount = pagingParameters.itemsPerPage;
                if(newCount + pagingParameters.itemsPerPage > pagingParameters.totalItemCount) 
                    { newCount = pagingParameters.totalItemCount - pagingParameters.newStart; }
                pagingParameters.newCount = newCount;
                // --------------------------------------------------------------
                // Turn on the buttons
                // --------------------------------------------------------------
                pagingParameters.previousIsActives = true;
                pagingParameters.nextIsActives = true;
                pagingParameters.topIsActives = true;
                pagingParameters.bottomIsActives = true;
                if (pagingParameters.newStart == 0) 
                {
                    pagingParameters.previousIsActives = false;
                    pagingParameters.topIsActives = false;
                }
                if (pagingParameters.newStart + pagingParameters.itemsPerPage >= pagingParameters.totalItemCount)
                {
                    pagingParameters.nextIsActives = false;
                    pagingParameters.bottomIsActives = false;
                }
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
            pagingParameters.previousStart = pagingParameters.newStart;
            ro.ReturnObj1 = pagingParameters; 
            return ro;
        }
    }
    public class PagingParameters
    {
        //Inputs
        public int previousStart =0;
        public int totalItemCount;
        public int itemsPerPage = 3;
        public string keyClicked ="";
        //Outputs 
        public int newStart;
        public int newCount;
        public bool previousIsActives;
        public bool nextIsActives;
        public bool topIsActives;
        public bool bottomIsActives;
        public string getPrevious()
        {
            if (previousIsActives)
            {
                return "";
            }
            return "Disabled";
        }
    }
    /******************************************************************************************
     * 
     * Paging
        - cshtml 
	        top 
	        next 
	        previous
	        bottom 
        - Session variables.
	        ItemCount
	        ItemsPerPage
	        LastQuery
        -- Paging Utility
        Inputs
	        previousStart
	        totalItemsInList
	        keyClicked
        Return
	        newStart
	        newCount
	        activeKeys
         IEnumerable<TEntity> data = 
 
         dbSet.OrderBy(order).Skip(skip).Take(take).ToList();
 
         <input type="submit" name="submitbutton1" value="submit1" />
         <input type="submit" name="submitbutton2" value="submit2" />
         Then in your default function you call the functions you want:
 
         if( Request.Form["submitbutton1"] != null)
         {
             // Code for function 1
         }
         else if(Request.Form["submitButton2"] != null )
         {
             // code for function 2
        }

     * 
     *******************************************************************************************/

}