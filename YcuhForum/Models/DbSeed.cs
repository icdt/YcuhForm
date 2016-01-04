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

                if(!db.AuthOptions.Any(a=>a.AuthOption_Name =="Admin"))
                {
                    AuthOptionModel authOptionModel = new AuthOptionModel();
                    authOptionModel.AuthOption_Id = Guid.NewGuid().ToString();
                    authOptionModel.AuthOption_Name = "Admin";
                    authOptionModel.AuthOption_CreateTime = DateTime.Now;
                    authOptionModel.AuthOption_UpdateTime = DateTime.Now;
                    authOptionModel.AuthOption_FK_UserId = "系統自建";
                    var authOptionObj = AuthOptionManager.ModelToDomain(authOptionModel);
                    AuthOptionManager.Create(authOptionObj);

                }

                if (!db.Users.Any(a => a.UserName == "icdt"))
                {
                    ApplicationUserModel accountModelObj = new ApplicationUserModel();
                    accountModelObj.UserName = "icdt";
                    accountModelObj.ApplicationUser_Name = "icdt";
                    accountModelObj.ApplicationUser_Password = "P@ssw0rd";
                    accountModelObj.ApplicationUser_AuthOption = Newtonsoft.Json.JsonConvert.SerializeObject(db.AuthOptions.First());
                    var accountObj = AUManager.ModelToDomain(accountModelObj);
                   AUManager.Create(accountObj);
                }
                if (!db.Users.Any(a => a.UserName == "icdt2"))
                {
                    ApplicationUserModel accountModelObj = new ApplicationUserModel();

                    accountModelObj.UserName = "icdt2";
                    accountModelObj.ApplicationUser_Name = "icdt2";
                    accountModelObj.ApplicationUser_Password = "P@ssw0rd";
                    accountModelObj.ApplicationUser_AuthOption = Newtonsoft.Json.JsonConvert.SerializeObject(db.AuthOptions.First());
                    var accountObj = AUManager.ModelToDomain(accountModelObj);
                    AUManager.Create(accountObj);
                }
            }
        }
    }
}