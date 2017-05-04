namespace Droplist.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRoleController : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Address", c => c.String());
        }
    }
}
