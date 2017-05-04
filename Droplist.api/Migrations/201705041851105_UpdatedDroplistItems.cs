namespace Droplist.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDroplistItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DroplistItems", "Quantity", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DroplistItems", "Quantity");
        }
    }
}
