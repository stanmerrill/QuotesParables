namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intialNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Favorite",
                c => new
                    {
                        QuoteCategoryId = c.Int(nullable: false, identity: true),
                        LogonAcountId = c.Int(nullable: false),
                        QuoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuoteCategoryId)
                .ForeignKey("dbo.LogonAccount", t => t.LogonAcountId, cascadeDelete: true)
                .ForeignKey("dbo.Quote", t => t.QuoteId, cascadeDelete: true)
                .Index(t => t.LogonAcountId)
                .Index(t => t.QuoteId);
            
            CreateTable(
                "dbo.LogonAccount",
                c => new
                    {
                        LogonAccountId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        EMail = c.String(maxLength: 100),
                        Password = c.String(maxLength: 100),
                        SecurityLevel = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.LogonAccountId);
            
            CreateTable(
                "dbo.Quote",
                c => new
                    {
                        QuoteId = c.Int(nullable: false, identity: true),
                        QuoteText = c.String(),
                        AuthorName = c.String(maxLength: 30),
                        Likes = c.Int(nullable: false),
                        QuoteTypeId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreatedByLogonAccountId = c.Int(nullable: false),
                        UpdatedByLogonAccountId = c.Int(nullable: false),
                        RatingTypeId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuoteId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.QuoteType", t => t.QuoteTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Rating", t => t.RatingTypeId, cascadeDelete: true)
                .Index(t => t.QuoteTypeId)
                .Index(t => t.CategoryId)
                .Index(t => t.RatingTypeId);
            
            CreateTable(
                "dbo.QuoteType",
                c => new
                    {
                        QuoteTypeId = c.Int(nullable: false, identity: true),
                        QuoteTypeDescription = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.QuoteTypeId);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingTypeDescription = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.RatingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorite", "QuoteId", "dbo.Quote");
            DropForeignKey("dbo.Quote", "RatingTypeId", "dbo.Rating");
            DropForeignKey("dbo.Quote", "QuoteTypeId", "dbo.QuoteType");
            DropForeignKey("dbo.Quote", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Favorite", "LogonAcountId", "dbo.LogonAccount");
            DropIndex("dbo.Quote", new[] { "RatingTypeId" });
            DropIndex("dbo.Quote", new[] { "CategoryId" });
            DropIndex("dbo.Quote", new[] { "QuoteTypeId" });
            DropIndex("dbo.Favorite", new[] { "QuoteId" });
            DropIndex("dbo.Favorite", new[] { "LogonAcountId" });
            DropTable("dbo.Rating");
            DropTable("dbo.QuoteType");
            DropTable("dbo.Quote");
            DropTable("dbo.LogonAccount");
            DropTable("dbo.Favorite");
            DropTable("dbo.Category");
        }
    }
}
