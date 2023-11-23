namespace InvoiceProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "InvoiceId" });
            DropTable("dbo.Invoices");
            DropTable("dbo.Items");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemCode = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        ItemName = c.String(),
                        Qty = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ItemCode);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false),
                        PayMentMethod = c.Boolean(nullable: false),
                        Customer = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
            CreateIndex("dbo.Items", "InvoiceId");
            AddForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices", "InvoiceId", cascadeDelete: true);
        }
    }
}
