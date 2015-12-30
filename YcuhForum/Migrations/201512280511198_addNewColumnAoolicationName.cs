namespace YcuhForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewColumnAoolicationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ApplicationUser_Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ApplicationUser_Name");
        }
    }
}
