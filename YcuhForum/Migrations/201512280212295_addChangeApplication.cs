namespace YcuhForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addChangeApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ApplicationUser_BackendAuth", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ApplicationUser_BackendAuth");
        }
    }
}
