namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extratextfields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quote", "QuoteText2", c => c.String());
            AddColumn("dbo.Quote", "QuoteText3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quote", "QuoteText3");
            DropColumn("dbo.Quote", "QuoteText2");
        }
    }
}
