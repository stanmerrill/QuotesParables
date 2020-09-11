namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropExtraTextfields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Quote", "QuoteTypeId", "dbo.QuoteType");
            DropForeignKey("dbo.Quote", "RatingTypeId", "dbo.Rating");
            DropIndex("dbo.Quote", new[] { "QuoteTypeId" });
            DropIndex("dbo.Quote", new[] { "RatingTypeId" });
            DropColumn("dbo.Quote", "QuoteText2");
            DropColumn("dbo.Quote", "QuoteText3");
            DropColumn("dbo.Quote", "QuoteTypeId");
            DropColumn("dbo.Quote", "RatingTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quote", "RatingTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Quote", "QuoteTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Quote", "QuoteText3", c => c.String());
            AddColumn("dbo.Quote", "QuoteText2", c => c.String());
            CreateIndex("dbo.Quote", "RatingTypeId");
            CreateIndex("dbo.Quote", "QuoteTypeId");
            AddForeignKey("dbo.Quote", "RatingTypeId", "dbo.Rating", "RatingId", cascadeDelete: true);
            AddForeignKey("dbo.Quote", "QuoteTypeId", "dbo.QuoteType", "QuoteTypeId", cascadeDelete: true);
        }
    }
}
