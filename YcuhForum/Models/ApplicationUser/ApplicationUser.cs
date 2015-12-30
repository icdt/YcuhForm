using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

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

        public string ApplicationUser_Name { get; set; }

        public string ApplicationUser_Department { get; set; }

        public string ApplicationUser_Job { get; set; }

        public string ApplicationUser_Gender { get; set; }

        public string ApplicationUser_Tel { get; set; }

        public string ApplicationUser_Address { get; set; }

        public bool ApplicationUser_IsActive { get; set; }

        public string ApplicationUser_AuthOption { get; set; }

        public bool ApplicationUser_DelLock { get; set; }

        public bool ApplicationUser_BackendAuth { get; set; }

        public DateTime ApplicationUser_CreateTime { get; set; }

        public DateTime ApplicationUser_UpdateTime { get; set; }

        public string ApplicationUser_FK_UpdateUserId { get; set; }


        [NotMapped]
        public string ApplicationUser_Password { get; set; }

    }
}