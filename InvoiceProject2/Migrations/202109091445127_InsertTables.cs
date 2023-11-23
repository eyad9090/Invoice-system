namespace InvoiceProject2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false),
                        PaymentMethod = c.Boolean(nullable: false),
                        Customer = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
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
                .PrimaryKey(t => t.ItemCode)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Items", new[] { "InvoiceId" });
            DropTable("dbo.Items");
            DropTable("dbo.Invoices");
        }
    }
}
