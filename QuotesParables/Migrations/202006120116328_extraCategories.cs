namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extraCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quote", "CategoryId2", c => c.Int(nullable: false));
            AddColumn("dbo.Quote", "CategoryId3", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quote", "CategoryId3");
            DropColumn("dbo.Quote", "CategoryId2");
        }
    }
}
