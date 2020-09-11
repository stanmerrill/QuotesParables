namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class droplookuptables : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.QuoteType");
            DropTable("dbo.Rating");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        RatingTypeDescription = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.RatingId);
            
            CreateTable(
                "dbo.QuoteType",
                c => new
                    {
                        QuoteTypeId = c.Int(nullable: false, identity: true),
                        QuoteTypeDescription = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.QuoteTypeId);
            
        }
    }
}
