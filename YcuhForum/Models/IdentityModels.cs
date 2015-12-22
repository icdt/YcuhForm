using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace YcuhForum.Models
{
    // 您可以在 ApplicationUser 類別新增更多屬性，為使用者新增設定檔資料，請造訪 http://go.microsoft.com/fwlink/?LinkID=317594 以深入了解。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }

        public string ApplicationUser_UserName { get; set; }

        public string ApplicationUser_Department { get; set; }

        public string ApplicationUser_Job { get; set; }

        public string ApplicationUser_Gender { get; set; }

        public string ApplicationUser_Tel { get; set; }

        public string ApplicationUser_Address { get; set; }

        public string ApplicationUser_IsActive { get; set; }

        public string ApplicationUser_AuthOption { get; set; }

        
        
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Examination> Examinations { get; set; }

        public DbSet<ExaminationRecord> ExaminationRecords { get; set; }

        public DbSet<ArticleUserRecord> ArticleUserRecords { get; set; }

        public DbSet<UserPoint> UserPoints { get; set; }

        public DbSet<PointCategory> PointCategorys { get; set; }


        public DbSet<ArticleUserReplay> ArticleUserReplays { get; set; }

        

    }
}