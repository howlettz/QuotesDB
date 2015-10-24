namespace Quotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Quotations", "Author", c => c.String(nullable: false));
            AlterColumn("dbo.Quotations", "Quote", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Quotations", "Quote", c => c.String());
            AlterColumn("dbo.Quotations", "Author", c => c.String());
        }
    }
}
