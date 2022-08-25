namespace QuotesParables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeFieldName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quote", "Approved", c => c.String(maxLength: 1));
            DropColumn("dbo.Quote", "Aprroved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quote", "Aprroved", c => c.String(maxLength: 1));
            DropColumn("dbo.Quote", "Approved");
        }
    }
}
