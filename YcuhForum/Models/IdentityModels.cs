using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace YcuhForum.Models
{
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

        public DbSet<ArticleGroup> ArticleGroups { get; set; }
        

        

    }
}