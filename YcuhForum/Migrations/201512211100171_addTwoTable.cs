namespace YcuhForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTwoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleUserReplays",
                c => new
                    {
                        ArticleUserReplay_Id = c.String(nullable: false, maxLength: 128),
                        ArticleUserReplay_Group = c.String(),
                        ArticleUserReplay_Category = c.String(),
                        ArticleUserReplay_Title = c.String(),
                        ArticleUserReplay_Content = c.String(),
                        ArticleUserReplay_DelLock = c.Boolean(nullable: false),
                        ArticleUserReplay_CreateTime = c.DateTime(nullable: false),
                        ArticleUserReplay_UpdateTime = c.DateTime(nullable: false),
                        ArticleUserReplay_FK_UserId = c.String(),
                    })
                .PrimaryKey(t => t.ArticleUserReplay_Id);
            
            CreateTable(
                "dbo.PointCategories",
                c => new
                    {
                        PointCategory_Id = c.String(nullable: false, maxLength: 128),
                        PointCategory_Name = c.String(),
                        PointCategory_DelLock = c.Boolean(nullable: false),
                        PointCategory_CreateTime = c.DateTime(nullable: false),
                        PointCategory_UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PointCategory_Id);
            
            AddColumn("dbo.Articles", "Article_Point", c => c.Int(nullable: false));
            AddColumn("dbo.ArticleUserRecords", "ArticleUserRecord_DelLock", c => c.Boolean(nullable: false));
            AddColumn("dbo.ArticleUserRecords", "ArticleUserRecord_FK_UserId", c => c.String());
            AddColumn("dbo.ArticleUserRecords", "ArticleUserRecord_FK_ArticleId", c => c.String());
            AddColumn("dbo.Examinations", "Examination_FK_ArticleId", c => c.String());
            AddColumn("dbo.Examinations", "Examination_FK_UserId", c => c.String());
            AddColumn("dbo.UserPoints", "UserPoint_FK_UserId", c => c.String());
            DropColumn("dbo.Articles", "Article_Name");
            DropColumn("dbo.Articles", "Article_FK_Point");
            DropColumn("dbo.ArticleUserRecords", "ArticleUserRecord_UserId");
            DropColumn("dbo.Examinations", "Examination_FK_Article");
            DropColumn("dbo.UserPoints", "UserPoint_FK_User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPoints", "UserPoint_FK_User", c => c.String());
            AddColumn("dbo.Examinations", "Examination_FK_Article", c => c.String());
            AddColumn("dbo.ArticleUserRecords", "ArticleUserRecord_UserId", c => c.String());
            AddColumn("dbo.Articles", "Article_FK_Point", c => c.String());
            AddColumn("dbo.Articles", "Article_Name", c => c.String());
            DropColumn("dbo.UserPoints", "UserPoint_FK_UserId");
            DropColumn("dbo.Examinations", "Examination_FK_UserId");
            DropColumn("dbo.Examinations", "Examination_FK_ArticleId");
            DropColumn("dbo.ArticleUserRecords", "ArticleUserRecord_FK_ArticleId");
            DropColumn("dbo.ArticleUserRecords", "ArticleUserRecord_FK_UserId");
            DropColumn("dbo.ArticleUserRecords", "ArticleUserRecord_DelLock");
            DropColumn("dbo.Articles", "Article_Point");
            DropTable("dbo.PointCategories");
            DropTable("dbo.ArticleUserReplays");
        }
    }
}
