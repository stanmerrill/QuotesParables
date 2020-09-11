namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linktoQuotetype : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Quote", "QuoteTypeId");
            AddForeignKey("dbo.Quote", "QuoteTypeId", "dbo.QuoteType", "QuoteTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quote", "QuoteTypeId", "dbo.QuoteType");
            DropIndex("dbo.Quote", new[] { "QuoteTypeId" });
        }
    }
}
