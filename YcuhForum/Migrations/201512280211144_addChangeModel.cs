namespace YcuhForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addChangeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthOptions",
                c => new
                    {
                        AuthOption_Id = c.String(nullable: false, maxLength: 128),
                        AuthOption_Name = c.String(),
                        AuthOption_DelLock = c.Boolean(nullable: false),
                        AuthOption_CreateTime = c.DateTime(nullable: false),
                        AuthOption_UpdateTime = c.DateTime(nullable: false),
                        AuthOption_FK_UpdateUserId = c.String(),
                        AuthOption_FK_UserId = c.String(),
                    })
                .PrimaryKey(t => t.AuthOption_Id);
            
            CreateTable(
                "dbo.ErrorRecords",
                c => new
                    {
                        ErrorRecord_Id = c.String(nullable: false, maxLength: 128),
                        ErrorRecord_SystemMessage = c.String(),
                        ErrorRecord_ActionDescribe = c.String(),
                        ErrorRecord_CustomedMessage = c.String(),
                        ErrorRecord_CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorRecord_Id);
            
            AddColumn("dbo.Articles", "Article_Count", c => c.Int(nullable: false));
            AddColumn("dbo.ArticleUserReplays", "ArticleUserReplay_FK_UpdateUserId", c => c.String());
            AddColumn("dbo.ArticleUserReplays", "ArticleUserReplay_ParentId", c => c.String());
            AddColumn("dbo.ExaminationRecords", "ExaminationRecord_DelLock", c => c.DateTime(nullable: false));
            AddColumn("dbo.ExaminationRecords", "ExaminationRecord_ArticleId", c => c.String());
            AlterColumn("dbo.UserPoints", "UserPoint_Point", c => c.String());
            DropColumn("dbo.ArticleUserReplays", "Article_FK_UpdateUserId");
            DropColumn("dbo.ExaminationRecords", "ExaminationRecord_ArticleGroup");
            DropColumn("dbo.ExaminationRecords", "ExaminationRecord_ArticleCategory");
            DropColumn("dbo.ExaminationRecords", "ExaminationRecord_ArticleTitle");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExaminationRecords", "ExaminationRecord_ArticleTitle", c => c.String());
            AddColumn("dbo.ExaminationRecords", "ExaminationRecord_ArticleCategory", c => c.String());
            AddColumn("dbo.ExaminationRecords", "ExaminationRecord_ArticleGroup", c => c.String());
            AddColumn("dbo.ArticleUserReplays", "Article_FK_UpdateUserId", c => c.String());
            AlterColumn("dbo.UserPoints", "UserPoint_Point", c => c.Int(nullable: false));
            DropColumn("dbo.ExaminationRecords", "ExaminationRecord_ArticleId");
            DropColumn("dbo.ExaminationRecords", "ExaminationRecord_DelLock");
            DropColumn("dbo.ArticleUserReplays", "ArticleUserReplay_ParentId");
            DropColumn("dbo.ArticleUserReplays", "ArticleUserReplay_FK_UpdateUserId");
            DropColumn("dbo.Articles", "Article_Count");
            DropTable("dbo.ErrorRecords");
            DropTable("dbo.AuthOptions");
        }
    }
}
