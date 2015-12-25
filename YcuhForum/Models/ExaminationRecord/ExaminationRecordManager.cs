using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ExaminationRecordManager
    {
        private static List<ExaminationRecord> _ExaminationRecordCache = new List<ExaminationRecord>();
        private static object _ExaminationRecordQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _ExaminationRecordCache = db.ExaminationRecords.ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<ExaminationRecord> GetAll()
        {
            return _ExaminationRecordCache.OrderBy(t => t.ExaminationRecord_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<ExaminationRecord> GetPagedList(int pageNumber, int pageSize)
        {
            return _ExaminationRecordCache.OrderBy(a => a.ExaminationRecord_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static ExaminationRecord Get(string id)
        {
            return _ExaminationRecordCache.FirstOrDefault(p => p.ExaminationRecord_Id == id);
        }

        //新增單一記錄
        public static void Create(ExaminationRecord ExaminationRecord)
        {
            Create(new List<ExaminationRecord>() { ExaminationRecord });
        }

        //新增多筆記錄
        public static void Create(List<ExaminationRecord> ExaminationRecords)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_ExaminationRecordQueueLock)
                {
                    db.ExaminationRecords.AddRange(ExaminationRecords);
                    db.SaveChanges();
                    //更新記憶体
                    _ExaminationRecordCache.AddRange(ExaminationRecords);
                }
            }
        }

        //更新一筆記錄
        public static void Update(ExaminationRecord ExaminationRecords)
        {
            Update(new List<ExaminationRecord>() { ExaminationRecords });
        }

        //更新多筆記錄
        public static void Update(List<ExaminationRecord> ExaminationRecords)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = ExaminationRecords.Select(a => a.ExaminationRecord_Id).ToList();
                var objInDB = db.ExaminationRecords.Where(a => objIDs.Contains(a.ExaminationRecord_Id)).ToList();

                foreach (ExaminationRecord item in objInDB)
                {
                    var theNewFromOutside = ExaminationRecords.FirstOrDefault(a => a.ExaminationRecord_Id == item.ExaminationRecord_Id);

                    Mapper.Map(theNewFromOutside, item);

                }

                lock (_ExaminationRecordQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _ExaminationRecordCache.RemoveAll(a => objIDs.Contains(a.ExaminationRecord_Id));
                    _ExaminationRecordCache.AddRange(ExaminationRecords);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(ExaminationRecord ExaminationRecord)
        {
            Remove(new List<ExaminationRecord>() { ExaminationRecord });
        }

        //刪除多筆記錄
        public static void Remove(List<ExaminationRecord> ExaminationRecords)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = ExaminationRecords.Select(a => a.ExaminationRecord_Id).ToList();
                var objInDB = db.ExaminationRecords.Where(a => objIDs.Contains(a.ExaminationRecord_Id)).ToList();

                lock (_ExaminationRecordQueueLock)
                {
                    db.SaveChanges();

                  
                    //更新記憶体
                    _ExaminationRecordCache.RemoveAll(a => objIDs.Contains(a.ExaminationRecord_Id));
                }
            }
        }
        #endregion


        #region 進階查詢

        public static ExaminationRecord GetExaminationRecordByArticleIdAndUserId(string articleId ,string userId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
              return  db.ExaminationRecords.Where(a => a.ExaminationRecord_ArticleId == articleId && a.ExaminationRecord_UserId == userId).FirstOrDefault();
            }
        }
        #endregion
    }
}