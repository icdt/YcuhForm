using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public static class DbSeed
    {
        public static void SeedManager()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!db.Users.Any(a => a.UserName == "icdt"))
                {
                    ApplicationUserModel accountModelObj = new ApplicationUserModel();

                    accountModelObj.UserName = "icdt";
                    accountModelObj.ApplicationUser_Name = "icdt";
                    accountModelObj.ApplicationUser_Password = "P@ssw0rd";
                    var accountObj = AUManager.ModelToDomain(accountModelObj);
                   AUManager.Create(accountObj);
                }
                if (!db.Users.Any(a => a.UserName == "icdt2"))
                {
                    ApplicationUserModel accountModelObj = new ApplicationUserModel();

                    accountModelObj.UserName = "icdt2";
                    accountModelObj.ApplicationUser_Name = "icdt2";
                    accountModelObj.ApplicationUser_Password = "P@ssw0rd";
                    var accountObj = AUManager.ModelToDomain(accountModelObj);
                    AUManager.Create(accountObj);
                }
            }
        }
    }
}