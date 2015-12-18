using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ArticleUserRecordManager
    {
        private static List<ArticleUserRecord> _ArticleUserRecordCache = new List<ArticleUserRecord>();
        private static object _ArticleUserRecordQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _ArticleUserRecordCache = db.ArticleUserRecords.ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<ArticleUserRecord> GetAll()
        {
            return _ArticleUserRecordCache.OrderBy(t => t.ArticleUserRecord_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<ArticleUserRecord> GetPagedList(int pageNumber, int pageSize)
        {
            return _ArticleUserRecordCache.OrderBy(a => a.ArticleUserRecord_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static ArticleUserRecord Get(string id)
        {
            return _ArticleUserRecordCache.FirstOrDefault(p => p.ArticleUserRecord_Id == id);
        }

        //新增單一記錄
        public static void Create(ArticleUserRecord ArticleUserRecord)
        {
            Create(new List<ArticleUserRecord>() { ArticleUserRecord });
        }

        //新增多筆記錄
        public static void Create(List<ArticleUserRecord> ArticleUserRecords)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_ArticleUserRecordQueueLock)
                {
                    db.ArticleUserRecords.AddRange(ArticleUserRecords);
                    db.SaveChanges();
                    //更新記憶体
                    _ArticleUserRecordCache.AddRange(ArticleUserRecords);
                }
            }
        }

        //更新一筆記錄
        public static void Update(ArticleUserRecord ArticleUserRecords)
        {
            Update(new List<ArticleUserRecord>() { ArticleUserRecords });
        }

        //更新多筆記錄
        public static void Update(List<ArticleUserRecord> ArticleUserRecords)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = ArticleUserRecords.Select(a => a.ArticleUserRecord_Id).ToList();
                var objInDB = db.ArticleUserRecords.Where(a => objIDs.Contains(a.ArticleUserRecord_Id)).ToList();

                foreach (ArticleUserRecord item in objInDB)
                {
                    var theNewFromOutside = ArticleUserRecords.FirstOrDefault(a => a.ArticleUserRecord_Id == item.ArticleUserRecord_Id);

                    Mapper.Map(theNewFromOutside, item);

                }

                lock (_ArticleUserRecordQueueLock)
                {
                    db.SaveChanges();

                    foreach (var item in ArticleUserRecords)
                    {
                        _ArticleUserRecordCache.Remove(item);
                    }
                    //更新記憶体
                    _ArticleUserRecordCache.AddRange(ArticleUserRecords);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(ArticleUserRecord ArticleUserRecord)
        {
            Remove(new List<ArticleUserRecord>() { ArticleUserRecord });
        }

        //刪除多筆記錄
        public static void Remove(List<ArticleUserRecord> ArticleUserRecords)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = ArticleUserRecords.Select(a => a.ArticleUserRecord_Id).ToList();
                var objInDB = db.ArticleUserRecords.Where(a => objIDs.Contains(a.ArticleUserRecord_Id)).ToList();

                lock (_ArticleUserRecordQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    foreach (var item in ArticleUserRecords)
                    {
                        _ArticleUserRecordCache.Remove(item);
                    }
                }
            }
        }
        #endregion
    }
}