using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class AUManager
    {
        private static List<ApplicationUser> _ApplicationUserCache = new List<ApplicationUser>();
        private static object _ApplicationUserQueueLock = new Object();
        private static ApplicationUserManager _userManager;

        public static ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            }
            private set
            {
                _userManager = value;
            }
        }

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _ApplicationUserCache = db.Users.Where(a => !a.ApplicationUser_DelLock).ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<ApplicationUser> GetAll()
        {
            return _ApplicationUserCache.OrderBy(t => t.ApplicationUser_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<ApplicationUser> GetPagedList(int pageNumber, int pageSize)
        {
            return _ApplicationUserCache.OrderBy(a => a.ApplicationUser_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static ApplicationUser Get(string id)
        {
            return _ApplicationUserCache.FirstOrDefault(p => p.Id == id);
        }

        //新增單一記錄
        public static void Create(ApplicationUser applicationUser)
        {
            Create(new List<ApplicationUser>() { applicationUser });
        }
        //新增多筆記錄
        public static void Create(List<ApplicationUser> applicationUser)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_ApplicationUserQueueLock)
                {
                    //未完
                    foreach (var item in applicationUser)
                    {
                        try
                        {
                            item.Id = Guid.NewGuid().ToString();
                            var result = UserManager.Create(item, item.ApplicationUser_Password);
                            if (result.Succeeded)
                            {
                                //更新記憶体
                                _ApplicationUserCache.Add(item);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                     
                    
                    }
                }
            }
        }


        //更新一筆記錄
        public static void Update(ApplicationUser ApplicationUsers)
        {
            Update(new List<ApplicationUser>() { ApplicationUsers });
        }

        //更新多筆記錄
        public static void Update(List<ApplicationUser> applicationUsers)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = applicationUsers.Select(a => a.Id).ToList();
                var objInDB = db.Users.Where(a => objIDs.Contains(a.Id)).ToList();

                foreach (ApplicationUser item in objInDB)
                {
                    var theNewFromOutside = applicationUsers.FirstOrDefault(a => a.Id == item.Id);
                    item.ApplicationUser_DelLock = theNewFromOutside.ApplicationUser_DelLock;
                    item.ApplicationUser_UpdateTime = DateTime.Now;

                }

                lock (_ApplicationUserQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _ApplicationUserCache.RemoveAll(a => objIDs.Contains(a.Id));
                    _ApplicationUserCache.AddRange(applicationUsers);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(ApplicationUser ApplicationUser)
        {
            Remove(new List<ApplicationUser>() { ApplicationUser });
        }

        //刪除多筆記錄
        public static void Remove(List<ApplicationUser> applicationUsers)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = applicationUsers.Select(a => a.Id).ToList();
                var objInDB = db.Users.Where(a => objIDs.Contains(a.Id)).ToList();

                foreach (var item in objInDB)
                {
                    item.ApplicationUser_DelLock = true;
                }

                lock (_ApplicationUserQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    foreach (var item in applicationUsers)
                    {
                        //有問題須修改
                        _ApplicationUserCache.Remove(item);
                    }
                }
            }
        }
        #endregion

        #region Domain & ViewModel 映射

        public static ApplicationUserModel DomainToModel(ApplicationUser ApplicationUser)
        {
            ApplicationUserModel viewModel = new ApplicationUserModel();
            Mapper.CreateMap<ApplicationUser, ApplicationUserModel>();
            viewModel = Mapper.Map<ApplicationUser, ApplicationUserModel>(ApplicationUser);

            return viewModel;
        }

        public static ApplicationUser ModelToDomain(ApplicationUserModel viewModel)
        {
            ApplicationUser ApplicationUser = new ApplicationUser();

            try
            {
                Mapper.CreateMap<ApplicationUserModel, ApplicationUser>();
                ApplicationUser = Mapper.Map<ApplicationUserModel, ApplicationUser>(viewModel);
            }
            catch (Exception e)
            {

            }


            return ApplicationUser;
        }

        #endregion
        #region 進階查詢

        public static ApplicationUser GetUserByUserName(string userName)
        {
            return _ApplicationUserCache.Where(a => a.UserName == userName).FirstOrDefault();
        }
        #endregion
    }
}