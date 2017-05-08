namespace Droplist.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBuildingIdfromProductTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "BuildingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "BuildingId", c => c.Int(nullable: false));
        }
    }
}
