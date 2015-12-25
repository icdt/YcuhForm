using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ArticleGroupManager
    {
        private static List<ArticleGroup> _ArticleGroupCache = new List<ArticleGroup>();
        private static object _ArticleGroupQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _ArticleGroupCache = db.ArticleGroups.AsNoTracking().Where(a => !a.ArticleGroup_DelLock).ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<ArticleGroup> GetAll()
        {
            return _ArticleGroupCache.OrderBy(t => t.ArticleGroup_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<ArticleGroup> GetPagedList(int pageNumber, int pageSize)
        {
            return _ArticleGroupCache.OrderBy(a => a.ArticleGroup_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static ArticleGroup Get(string id)
        {
            return _ArticleGroupCache.FirstOrDefault(p => p.ArticleGroup_Id == id);
        }

        //新增單一記錄
        public static void Create(ArticleGroup ArticleGroup)
        {
            Create(new List<ArticleGroup>() { ArticleGroup });
        }

        //新增多筆記錄
        public static void Create(List<ArticleGroup> ArticleGroups)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_ArticleGroupQueueLock)
                {
                    db.ArticleGroups.AddRange(ArticleGroups);
                    db.SaveChanges();
                    //更新記憶体
                    _ArticleGroupCache.AddRange(ArticleGroups);
                }
            }
        }

        //更新一筆記錄
        public static void Update(ArticleGroup ArticleGroups)
        {
            Update(new List<ArticleGroup>() { ArticleGroups });
        }

        //更新多筆記錄
        public static void Update(List<ArticleGroup> ArticleGroups)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = ArticleGroups.Select(a => a.ArticleGroup_Id).ToList();
                var objInDB = db.ArticleGroups.Where(a => objIDs.Contains(a.ArticleGroup_Id)).ToList();

                foreach (ArticleGroup item in objInDB)
                {
                    var theNewFromOutside = ArticleGroups.FirstOrDefault(a => a.ArticleGroup_Id == item.ArticleGroup_Id);

                    Mapper.Map(theNewFromOutside, item);

                }

                lock (_ArticleGroupQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _ArticleGroupCache.RemoveAll(a => objIDs.Contains(a.ArticleGroup_Id));
                    _ArticleGroupCache.AddRange(ArticleGroups);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(ArticleGroup ArticleGroup)
        {
            Remove(new List<ArticleGroup>() { ArticleGroup });
        }

        //刪除多筆記錄
        public static void Remove(List<ArticleGroup> ArticleGroups)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = ArticleGroups.Select(a => a.ArticleGroup_Id).ToList();
                var objInDB = db.ArticleGroups.Where(a => objIDs.Contains(a.ArticleGroup_Id)).ToList();

                lock (_ArticleGroupQueueLock)
                {
                    db.SaveChanges();

                   
                    //更新記憶体
                    _ArticleGroupCache.RemoveAll(a => objIDs.Contains(a.ArticleGroup_Id));
                }
            }
        }
        #endregion

        #region Domain & ViewModel 映射

        public static ArticleGroupModel DomainToModel(ArticleGroup articleGroup)
        {
            ArticleGroupModel viewModel = new ArticleGroupModel();
            Mapper.CreateMap<ArticleGroup, ArticleGroupModel>();
            viewModel = Mapper.Map<ArticleGroup, ArticleGroupModel>(articleGroup);

            return viewModel;
        }

        public static ArticleGroup ModelToDomain(ArticleGroupModel viewModel)
        {
            ArticleGroup articleGroup = new ArticleGroup();

            try
            {
                Mapper.CreateMap<ArticleGroupModel, ArticleGroup>();
                articleGroup = Mapper.Map<ArticleGroupModel, ArticleGroup>(viewModel);
            }
            catch (Exception e)
            {

            }


            return articleGroup;
        }

        #endregion
    }
}