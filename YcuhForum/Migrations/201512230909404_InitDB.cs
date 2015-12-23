namespace YcuhForum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleGroups",
                c => new
                    {
                        ArticleGroup_Id = c.String(nullable: false, maxLength: 128),
                        ArticleGroup_Name = c.String(),
                        ArticleGroup_DelLock = c.Boolean(nullable: false),
                        ArticleGroup_FK_UserId = c.String(),
                        ArticleGroup_CreateTime = c.DateTime(nullable: false),
                        Article_FK_UpdateUserId = c.String(),
                        ArticleGroup_UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleGroup_Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Article_Id = c.String(nullable: false, maxLength: 128),
                        Article_Title = c.String(),
                        Article_Content = c.String(),
                        Article_Point = c.Int(nullable: false),
                        Article_FileUrl = c.String(),
                        Article_IsShow = c.Boolean(nullable: false),
                        Article_IsReply = c.Boolean(nullable: false),
                        Article_OtherSiteUrl = c.String(),
                        Article_DelLock = c.Boolean(nullable: false),
                        Article_CreateTime = c.DateTime(nullable: false),
                        Article_UpdateTime = c.DateTime(nullable: false),
                        Article_FK_UpdateUserId = c.String(),
                        Article_FK_UserId = c.String(),
                        Article_FK_ArticleGroupId = c.String(),
                        Article_FK_PointCategoryId = c.String(),
                    })
                .PrimaryKey(t => t.Article_Id);
            
            CreateTable(
                "dbo.ArticleUserRecords",
                c => new
                    {
                        ArticleUserRecord_Id = c.String(nullable: false, maxLength: 128),
                        ArticleUserRecord_IsEnforce = c.Boolean(nullable: false),
                        ArticleUserRecord_DelLock = c.Boolean(nullable: false),
                        ArticleUserRecord_CreateTime = c.DateTime(nullable: false),
                        ArticleUserRecord_UpdateTime = c.DateTime(nullable: false),
                        ArticleUserRecord_FK_UserId = c.String(),
                        ArticleUserRecord_FK_UpdateUserId = c.String(),
                        ArticleUserRecord_FK_ArticleId = c.String(),
                    })
                .PrimaryKey(t => t.ArticleUserRecord_Id);
            
            CreateTable(
                "dbo.ArticleUserReplays",
                c => new
                    {
                        ArticleUserReplay_Id = c.String(nullable: false, maxLength: 128),
                        ArticleUserReplay_Content = c.String(),
                        ArticleUserReplay_DelLock = c.Boolean(nullable: false),
                        ArticleUserReplay_CreateTime = c.DateTime(nullable: false),
                        ArticleUserReplay_UpdateTime = c.DateTime(nullable: false),
                        ArticleUserReplay_FK_UserId = c.String(),
                        Article_FK_UpdateUserId = c.String(),
                        ArticleUserReplay_FK_ArticleId = c.String(),
                    })
                .PrimaryKey(t => t.ArticleUserReplay_Id);
            
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
                "dbo.Examinations",
                c => new
                    {
                        Examination_Id = c.String(nullable: false, maxLength: 128),
                        Examination_TestedUserIdList = c.String(),
                        Examination_Question = c.String(),
                        Examination_QuestionNumber = c.Int(nullable: false),
                        Examination_FileUrl = c.String(),
                        Examination_DelLock = c.Boolean(nullable: false),
                        Examination_CreateTime = c.DateTime(nullable: false),
                        Examination_UpdateTime = c.DateTime(nullable: false),
                        Examination_FK_UserId = c.String(),
                        Examination_FK_UpdateUserId = c.String(),
                        Examination_FK_ArticleId = c.String(),
                    })
                .PrimaryKey(t => t.Examination_Id);
            
            CreateTable(
                "dbo.PointCategories",
                c => new
                    {
                        PointCategory_Id = c.String(nullable: false, maxLength: 128),
                        PointCategory_Name = c.String(),
                        PointCategory_DelLock = c.Boolean(nullable: false),
                        PointCategory_CreateTime = c.DateTime(nullable: false),
                        PointCategory_UpdateTime = c.DateTime(nullable: false),
                        Article_FK_UserId = c.String(),
                        Article_FK_UpdateUserId = c.String(),
                    })
                .PrimaryKey(t => t.PointCategory_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserPoints",
                c => new
                    {
                        UserPoint_Id = c.String(nullable: false, maxLength: 128),
                        UserPoint_Point = c.Int(nullable: false),
                        UserPoint_CreateTime = c.DateTime(nullable: false),
                        UserPoint_UpdateTime = c.DateTime(nullable: false),
                        UserPoint_FK_UserId = c.String(),
                    })
                .PrimaryKey(t => t.UserPoint_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Department = c.String(),
                        ApplicationUser_Job = c.String(),
                        ApplicationUser_Gender = c.String(),
                        ApplicationUser_Tel = c.String(),
                        ApplicationUser_Address = c.String(),
                        ApplicationUser_IsActive = c.String(),
                        ApplicationUser_AuthOption = c.String(),
                        ApplicationUser_DelLock = c.Boolean(nullable: false),
                        ApplicationUser_CreateTime = c.DateTime(nullable: false),
                        ApplicationUser_UpdateTime = c.DateTime(nullable: false),
                        ApplicationUser_FK_UpdateUserId = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserPoints");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PointCategories");
            DropTable("dbo.Examinations");
            DropTable("dbo.ExaminationRecords");
            DropTable("dbo.ArticleUserReplays");
            DropTable("dbo.ArticleUserRecords");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleGroups");
        }
    }
}
