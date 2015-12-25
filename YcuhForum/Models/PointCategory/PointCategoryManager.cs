using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class PointCategoryManager
    {
        private static List<PointCategory> _PointCategoryCache = new List<PointCategory>();
        private static object _PointCategoryQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _PointCategoryCache = db.PointCategorys.ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<PointCategory> GetAll()
        {
            return _PointCategoryCache.OrderBy(t => t.PointCategory_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<PointCategory> GetPagedList(int pageNumber, int pageSize)
        {
            return _PointCategoryCache.OrderBy(a => a.PointCategory_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static PointCategory Get(string id)
        {
            return _PointCategoryCache.FirstOrDefault(p => p.PointCategory_Id == id);
        }

        //新增單一記錄
        public static void Create(PointCategory PointCategory)
        {
            Create(new List<PointCategory>() { PointCategory });
        }

        //新增多筆記錄
        public static void Create(List<PointCategory> PointCategorys)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_PointCategoryQueueLock)
                {
                    db.PointCategorys.AddRange(PointCategorys);
                    db.SaveChanges();
                    //更新記憶体
                    _PointCategoryCache.AddRange(PointCategorys);
                }
            }
        }

        //更新一筆記錄
        public static void Update(PointCategory PointCategorys)
        {
            Update(new List<PointCategory>() { PointCategorys });
        }

        //更新多筆記錄
        public static void Update(List<PointCategory> PointCategorys)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = PointCategorys.Select(a => a.PointCategory_Id).ToList();
                var objInDB = db.PointCategorys.Where(a => objIDs.Contains(a.PointCategory_Id)).ToList();

                foreach (PointCategory item in objInDB)
                {
                    var theNewFromOutside = PointCategorys.FirstOrDefault(a => a.PointCategory_Id == item.PointCategory_Id);

                    Mapper.Map(theNewFromOutside, item);

                }

                lock (_PointCategoryQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _PointCategoryCache.RemoveAll(a => objIDs.Contains(a.PointCategory_Id));
                    _PointCategoryCache.AddRange(PointCategorys);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(PointCategory PointCategory)
        {
            Remove(new List<PointCategory>() { PointCategory });
        }

        //刪除多筆記錄
        public static void Remove(List<PointCategory> PointCategorys)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = PointCategorys.Select(a => a.PointCategory_Id).ToList();
                var objInDB = db.PointCategorys.Where(a => objIDs.Contains(a.PointCategory_Id)).ToList();

                lock (_PointCategoryQueueLock)
                {
                    db.SaveChanges();

                   
                    //更新記憶体
                    _PointCategoryCache.RemoveAll(a => objIDs.Contains(a.PointCategory_Id));
                }
            }
        }
        #endregion

        #region Domain & ViewModel 映射

        public static PointCategoryModel DomainToModel(PointCategory pointCategory)
        {
            PointCategoryModel viewModel = new PointCategoryModel();
            Mapper.CreateMap<PointCategory, PointCategoryModel>();
            viewModel = Mapper.Map<PointCategory, PointCategoryModel>(pointCategory);

            return viewModel;
        }

        public static PointCategory ModelToDomain(PointCategoryModel viewModel)
        {
            PointCategory pointCategory = new PointCategory();

            try
            {
                Mapper.CreateMap<PointCategoryModel, PointCategory>();
                pointCategory = Mapper.Map<PointCategoryModel, PointCategory>(viewModel);
            }
            catch (Exception e)
            {

            }


            return pointCategory;
        }

        #endregion
    }
}