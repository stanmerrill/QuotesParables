namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatypekeyadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuoteType",
                c => new
                    {
                        QuoteTypeId = c.Int(nullable: false, identity: true),
                        QuoteTypeDescription = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.QuoteTypeId);
            
            AddColumn("dbo.Quote", "QuoteTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quote", "QuoteTypeId");
            DropTable("dbo.QuoteType");
        }
    }
}
