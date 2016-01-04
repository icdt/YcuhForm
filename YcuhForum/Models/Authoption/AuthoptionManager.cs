using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class AuthOptionManager
    {
        private static List<AuthOption> _AuthOptionCache = new List<AuthOption>();
        private static object _AuthOptionQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _AuthOptionCache = db.AuthOptions.AsNoTracking().ToList();
            }
        }
        #endregion



        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<AuthOption> GetAll()
        {
            return _AuthOptionCache.OrderBy(t => t.AuthOption_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<AuthOption> GetPagedList(int pageNumber, int pageSize)
        {
            return _AuthOptionCache.OrderBy(a => a.AuthOption_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static AuthOption Get(string id)
        {
            return _AuthOptionCache.FirstOrDefault(p => p.AuthOption_Id == id);
        }

        //新增單一記錄
        public static void Create(AuthOption AuthOption)
        {
            Create(new List<AuthOption>() { AuthOption });
        }

        //新增多筆記錄
        public static void Create(List<AuthOption> AuthOptions)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_AuthOptionQueueLock)
                {
                    db.AuthOptions.AddRange(AuthOptions);
                    db.SaveChanges();
                    //更新記憶体
                    _AuthOptionCache.AddRange(AuthOptions);
                }
            }
        }

        //更新一筆記錄
        public static void Update(AuthOption AuthOptions)
        {
            Update(new List<AuthOption>() { AuthOptions });
        }

        //更新多筆記錄
        public static void Update(List<AuthOption> AuthOptions)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = AuthOptions.Select(a => a.AuthOption_Id).ToList();
                var objInDB = db.AuthOptions.Where(a => objIDs.Contains(a.AuthOption_Id)).ToList();

                foreach (AuthOption item in objInDB)
                {
                    var theNewFromOutside = AuthOptions.FirstOrDefault(a => a.AuthOption_Id == item.AuthOption_Id);

                    item.AuthOption_Name = theNewFromOutside.AuthOption_Name;
                    item.AuthOption_DelLock = theNewFromOutside.AuthOption_DelLock;
                    item.AuthOption_FK_UpdateUserId = theNewFromOutside.AuthOption_FK_UpdateUserId;
                    item.AuthOption_UpdateTime = DateTime.Now;
                }

                lock (_AuthOptionQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _AuthOptionCache.RemoveAll(a => objIDs.Contains(a.AuthOption_Id));
                    _AuthOptionCache.AddRange(AuthOptions);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(AuthOption AuthOption)
        {
            Remove(new List<AuthOption>() { AuthOption });
        }

        //刪除多筆記錄
        public static void Remove(List<AuthOption> AuthOptions)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = AuthOptions.Select(a => a.AuthOption_Id).ToList();
                var objInDB = db.AuthOptions.Where(a => objIDs.Contains(a.AuthOption_Id)).ToList();

                lock (_AuthOptionQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _AuthOptionCache.RemoveAll(a => objIDs.Contains(a.AuthOption_Id));
                }
            }
        }
        #endregion




        #region Domain & ViewModel 映射

        public static AuthOptionModel DomainToModel(AuthOption AuthOption)
        {
            AuthOptionModel viewModel = new AuthOptionModel();
            Mapper.CreateMap<AuthOption, AuthOptionModel>();
            viewModel = Mapper.Map<AuthOption, AuthOptionModel>(AuthOption);

            return viewModel;
        }

        public static AuthOption ModelToDomain(AuthOptionModel viewModel)
        {
            AuthOption AuthOption = new AuthOption();

            try
            {
                Mapper.CreateMap<AuthOptionModel, AuthOption>();
                AuthOption = Mapper.Map<AuthOptionModel, AuthOption>(viewModel);
            }
            catch (Exception e)
            {

            }


            return AuthOption;
        }


        public static List<AuthOptionModel> DomainToModel(List<AuthOption> AuthOption)
        {
            List<AuthOptionModel> viewModel;
            Mapper.CreateMap<List<AuthOption>, List<AuthOptionModel>>();
            viewModel = Mapper.Map<List<AuthOption>, List<AuthOptionModel>>(AuthOption);

            return viewModel;
        }
        #endregion

        #region 進階查詢

        public static List<AuthOption> GetAuthoptionByIdList(List<string> idList)
        {
           return _AuthOptionCache.Where(a => idList.Any(b => a.AuthOption_Id == b)).ToList();
        } 

        #endregion

    }
}