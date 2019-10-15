namespace FossERp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Company = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        City = c.String(),
                        State = c.String(nullable: false),
                        Zip = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                        JobTitle = c.String(),
                        Mobile = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Department = c.String(),
                        Position = c.String(),
                        Address = c.String(),
                        Location = c.String(),
                        BankAccount = c.String(),
                        BankIFSC = c.String(),
                        Gender = c.String(),
                        DateofBirth = c.DateTime(nullable: false),
                        IdentificationNo = c.String(),
                        CertificateLavel = c.String(),
                        FieldofStudy = c.String(),
                        Document = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Payroll",
                c => new
                    {
                        PayrollID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentType = c.String(nullable: false),
                        ImagePath = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PayrollID)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Currency = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Purpose = c.String(nullable: false),
                        DateOfSpend = c.DateTime(nullable: false),
                        Marchant = c.String(),
                        Category = c.String(nullable: false),
                        Description = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Leads",
                c => new
                    {
                        LeadID = c.Int(nullable: false, identity: true),
                        LeadCreationDate = c.DateTime(nullable: false),
                        ContactName = c.String(nullable: false),
                        Email = c.String(),
                        Mobile = c.String(nullable: false),
                        CompanyName = c.String(nullable: false),
                        CompanyEmail = c.String(),
                        CompanyMobile = c.String(),
                        Address = c.String(nullable: false),
                        LandMark = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Pincode = c.String(nullable: false),
                        LeadCreatorName = c.String(),
                        Source = c.String(),
                        BusinessCategory = c.String(nullable: false),
                        OurProduct = c.String(),
                    })
                .PrimaryKey(t => t.LeadID);
            
            CreateTable(
                "dbo.SalesOrder",
                c => new
                    {
                        SalesOrderID = c.Int(nullable: false, identity: true),
                        SaleConfirmationDate = c.String(),
                        CompanyName = c.String(),
                        SalesPerson = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MIBStatus = c.String(),
                        MIBHold = c.String(),
                        activation = c.String(),
                        LeadID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesOrderID)
                .ForeignKey("dbo.Leads", t => t.LeadID, cascadeDelete: true)
                .Index(t => t.LeadID);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(),
                        CustomerID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        SalesStartDate = c.DateTime(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        HSNORSACCode = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseOrderProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        PurchaseOrderId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.PurchaseOrderId);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        VendorID = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.PurchaseOrderId)
                .ForeignKey("dbo.Vendors", t => t.VendorID, cascadeDelete: true)
                .Index(t => t.VendorID);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNo = c.String(),
                        EmailAddress = c.String(),
                        Gst = c.Decimal(precision: 18, scale: 2),
                        OpeningBalance = c.Decimal(precision: 18, scale: 2),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.VendorID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PurchaseOrder", "VendorID", "dbo.Vendors");
            DropForeignKey("dbo.PurchaseOrderProducts", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.SalesOrder", "LeadID", "dbo.Leads");
            DropForeignKey("dbo.Payroll", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PurchaseOrder", new[] { "VendorID" });
            DropIndex("dbo.PurchaseOrderProducts", new[] { "PurchaseOrderId" });
            DropIndex("dbo.PurchaseOrderProducts", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.SalesOrder", new[] { "LeadID" });
            DropIndex("dbo.Payroll", new[] { "EmployeeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Vendors");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.PurchaseOrderProducts");
            DropTable("dbo.Products");
            DropTable("dbo.Order");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.SalesOrder");
            DropTable("dbo.Leads");
            DropTable("dbo.Expenses");
            DropTable("dbo.Payroll");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
        }
    }
}
