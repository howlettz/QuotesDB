namespace Quotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Quotations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Quote = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Quotations", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Quotations", new[] { "CategoryID" });
            DropTable("dbo.Quotations");
            DropTable("dbo.Categories");
        }
    }
}
