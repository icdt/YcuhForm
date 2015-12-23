using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class UserPointManager
    {
        private static List<UserPoint> _UserPointCache = new List<UserPoint>();
        private static object _UserPointQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _UserPointCache = db.UserPoints.ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<UserPoint> GetAll()
        {
            return _UserPointCache.OrderBy(t => t.UserPoint_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<UserPoint> GetPagedList(int pageNumber, int pageSize)
        {
            return _UserPointCache.OrderBy(a => a.UserPoint_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static UserPoint Get(string id)
        {
            return _UserPointCache.FirstOrDefault(p => p.UserPoint_Id == id);
        }

        //新增單一記錄
        public static void Create(UserPoint UserPoint)
        {
            Create(new List<UserPoint>() { UserPoint });
        }

        //新增多筆記錄
        public static void Create(List<UserPoint> UserPoints)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_UserPointQueueLock)
                {
                    db.UserPoints.AddRange(UserPoints);
                    db.SaveChanges();
                    //更新記憶体
                    _UserPointCache.AddRange(UserPoints);
                }
            }
        }

        //更新一筆記錄
        public static void Update(UserPoint UserPoints)
        {
            Update(new List<UserPoint>() { UserPoints });
        }

        //更新多筆記錄
        public static void Update(List<UserPoint> UserPoints)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = UserPoints.Select(a => a.UserPoint_Id).ToList();
                var objInDB = db.UserPoints.Where(a => objIDs.Contains(a.UserPoint_Id)).ToList();

                foreach (UserPoint item in objInDB)
                {
                    var theNewFromOutside = UserPoints.FirstOrDefault(a => a.UserPoint_Id == item.UserPoint_Id);

                    Mapper.Map(theNewFromOutside, item);

                }

                lock (_UserPointQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _UserPointCache.RemoveAll(a => objIDs.Contains(a.UserPoint_Id));
                    _UserPointCache.AddRange(UserPoints);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(UserPoint UserPoint)
        {
            Remove(new List<UserPoint>() { UserPoint });
        }

        //刪除多筆記錄
        public static void Remove(List<UserPoint> UserPoints)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = UserPoints.Select(a => a.UserPoint_Id).ToList();
                var objInDB = db.UserPoints.Where(a => objIDs.Contains(a.UserPoint_Id)).ToList();

                lock (_UserPointQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    foreach (var item in UserPoints)
                    {
                        _UserPointCache.Remove(item);
                    }
                }
            }
        }
        #endregion
    }
}