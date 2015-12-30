namespace YcuhForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCCHANGE : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "ApplicationUser_IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "ApplicationUser_IsActive", c => c.String());
        }
    }
}
