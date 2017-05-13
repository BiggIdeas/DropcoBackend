namespace Droplist.api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompletedOnDateOnDroplistModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Droplists", "CompletedOnDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Droplists", "CompletedOnDate");
        }
    }
}
