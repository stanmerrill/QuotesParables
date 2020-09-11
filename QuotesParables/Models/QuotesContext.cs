using System.Data.Entity;
using QuotesParables.Utilities;
namespace QuotesParables.Models
{
    //------------------------------------------
    //    Package Manager console
    //------------------------------------------
    //enable-migrations
    //add-migration InitialCreate
    //Update-Database
    public class QuotesContext : DbContext
    {
        //            : base("name=QuotesNewContex")
        //            : base("name=QuotesContext")
        //            : base("name=QuoteTestContext")
        //-----------------------------------------
        // MODIFY UTILITY GLOBALS ALSO!!!!!   
        //-----------------------------------------
        public QuotesContext()
                    : base("name=QuotesContext")
        {
        }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LogonAccount> LogonAccounts { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<QuoteType> QuoteType { get; set; }
    }
}
