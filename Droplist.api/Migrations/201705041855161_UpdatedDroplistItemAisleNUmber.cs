namespace Droplist.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDroplistItemAisleNUmber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DroplistItems", "AisleNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DroplistItems", "AisleNumber", c => c.String());
        }
    }
}
