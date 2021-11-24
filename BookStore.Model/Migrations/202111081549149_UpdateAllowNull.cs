namespace BookStore.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAllowNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Book", "Status", c => c.Int());
            AlterColumn("dbo.Book", "Quantity", c => c.Int());
            AlterColumn("dbo.Book", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Book", "DateModified", c => c.DateTime());
            AlterColumn("dbo.OrderDetails", "Quantity", c => c.Int());
            AlterColumn("dbo.OrderDetails", "UnitPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetails", "BookId", c => c.Int());
            AlterColumn("dbo.Order", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Order", "Status", c => c.Int());
            AlterColumn("dbo.Order", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Order", "DateModified", c => c.DateTime());
            AlterColumn("dbo.Order", "Cost", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Order", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Order", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Order", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Order", "PhoneNumber", c => c.Byte(nullable: false));
            AlterColumn("dbo.OrderDetails", "BookId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetails", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Book", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Book", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Book", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Book", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Book", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
