namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newFieldAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quote", "Aprroved", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quote", "Aprroved");
        }
    }
}
