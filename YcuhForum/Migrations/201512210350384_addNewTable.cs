namespace YcuhForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleUserRecords",
                c => new
                    {
                        ArticleUserRecord_Id = c.String(nullable: false, maxLength: 128),
                        ArticleUserRecord_ArticleGroup = c.String(),
                        ArticleUserRecord_ArticleCategory = c.String(),
                        ArticleUserRecord_ArticleTitle = c.String(),
                        ArticleUserRecord_CreateTime = c.DateTime(nullable: false),
                        ArticleUserRecord_UpdateTime = c.DateTime(nullable: false),
                        ArticleUserRecord_UserId = c.String(),
                    })
                .PrimaryKey(t => t.ArticleUserRecord_Id);
            
            CreateTable(
                "dbo.ExaminationRecords",
                c => new
                    {
                        ExaminationRecord_Id = c.String(nullable: false, maxLength: 128),
                        ExaminationRecord_ArticleGroup = c.String(),
                        ExaminationRecord_ArticleCategory = c.String(),
                        ExaminationRecord_ArticleTitle = c.String(),
                        ExaminationRecord_ExaminationQuestionNumber = c.String(),
                        ExaminationRecord_CorrentNumber = c.String(),
                        ExaminationRecord_ErrorNumber = c.String(),
                        ExaminationRecord_IsPass = c.Boolean(nullable: false),
                        ExaminationRecord_CreateTime = c.DateTime(nullable: false),
                        ExaminationRecord_UpdateTime = c.DateTime(nullable: false),
                        ExaminationRecord_UserId = c.String(),
                    })
                .PrimaryKey(t => t.ExaminationRecord_Id);
            
            CreateTable(
                "dbo.UserPoints",
                c => new
                    {
                        UserPoint_Id = c.String(nullable: false, maxLength: 128),
                        UserPoint_Point = c.String(),
                        UserPoint_CreateTime = c.DateTime(nullable: false),
                        UserPoint_UpdateTime = c.DateTime(nullable: false),
                        UserPoint_FK_User = c.String(),
                    })
                .PrimaryKey(t => t.UserPoint_Id);
            
            AddColumn("dbo.Articles", "Article_DelLock", c => c.Boolean(nullable: false));
            AddColumn("dbo.Examinations", "Examination_CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Examinations", "Examination_UpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Examinations", "Examination_UpdateTime");
            DropColumn("dbo.Examinations", "Examination_CreateTime");
            DropColumn("dbo.Articles", "Article_DelLock");
            DropTable("dbo.UserPoints");
            DropTable("dbo.ExaminationRecords");
            DropTable("dbo.ArticleUserRecords");
        }
    }
}
