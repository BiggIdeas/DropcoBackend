namespace Droplist.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Droplists", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Products", "BuildingId", "dbo.Buildings");
            DropForeignKey("dbo.Sections", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.Droplists", new[] { "DepartmentId" });
            DropIndex("dbo.Sections", new[] { "BuildingId" });
            DropIndex("dbo.Products", new[] { "BuildingId" });
            RenameColumn(table: "dbo.Droplists", name: "EmployeeId", newName: "DriverId");
            RenameIndex(table: "dbo.Droplists", name: "IX_EmployeeId", newName: "IX_DriverId");
            AddColumn("dbo.Droplists", "StockerId", c => c.Int(nullable: false));
            AddColumn("dbo.Droplists", "SectionId", c => c.Int(nullable: false));
            AddColumn("dbo.Droplists", "Stocker_EmployeeId", c => c.Int());
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String());
            CreateIndex("dbo.Droplists", "SectionId");
            CreateIndex("dbo.Droplists", "Stocker_EmployeeId");
            AddForeignKey("dbo.Droplists", "SectionId", "dbo.Sections", "SectionId", cascadeDelete: true);
            AddForeignKey("dbo.Droplists", "Stocker_EmployeeId", "dbo.Employees", "EmployeeId");
            DropColumn("dbo.Droplists", "DepartmentId");
            DropColumn("dbo.Droplists", "Description");
            DropColumn("dbo.Departments", "BuildingId");
            DropColumn("dbo.Sections", "BuildingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "BuildingId", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "BuildingId", c => c.Int(nullable: false));
            AddColumn("dbo.Droplists", "Description", c => c.String());
            AddColumn("dbo.Droplists", "DepartmentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Droplists", "Stocker_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Droplists", "SectionId", "dbo.Sections");
            DropIndex("dbo.Droplists", new[] { "Stocker_EmployeeId" });
            DropIndex("dbo.Droplists", new[] { "SectionId" });
            AlterColumn("dbo.Departments", "DepartmentName", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.Droplists", "Stocker_EmployeeId");
            DropColumn("dbo.Droplists", "SectionId");
            DropColumn("dbo.Droplists", "StockerId");
            RenameIndex(table: "dbo.Droplists", name: "IX_DriverId", newName: "IX_EmployeeId");
            RenameColumn(table: "dbo.Droplists", name: "DriverId", newName: "EmployeeId");
            CreateIndex("dbo.Products", "BuildingId");
            CreateIndex("dbo.Sections", "BuildingId");
            CreateIndex("dbo.Droplists", "DepartmentId");
            AddForeignKey("dbo.Sections", "BuildingId", "dbo.Buildings", "BuildingId", cascadeDelete: true);
            AddForeignKey("dbo.Products", "BuildingId", "dbo.Buildings", "BuildingId");
            AddForeignKey("dbo.Droplists", "DepartmentId", "dbo.Departments", "DepartmentId", cascadeDelete: true);
        }
    }
}
