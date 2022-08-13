namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quote", "Contributor", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quote", "Contributor");
        }
    }
}
