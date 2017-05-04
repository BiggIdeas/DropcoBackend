namespace Droplist.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDroplistDriverRelationshipOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Droplists", "DriverId", "dbo.Employees");
            DropIndex("dbo.Droplists", new[] { "DriverId" });
            AlterColumn("dbo.Droplists", "DriverId", c => c.Int());
            CreateIndex("dbo.Droplists", "DriverId");
            AddForeignKey("dbo.Droplists", "DriverId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Droplists", "DriverId", "dbo.Employees");
            DropIndex("dbo.Droplists", new[] { "DriverId" });
            AlterColumn("dbo.Droplists", "DriverId", c => c.Int(nullable: false));
            CreateIndex("dbo.Droplists", "DriverId");
            AddForeignKey("dbo.Droplists", "DriverId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
