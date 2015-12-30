using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ArticleUserReplayManager
    {
        private static List<ArticleUserReplay> _ArticleUserReplayCache = new List<ArticleUserReplay>();
        private static object _ArticleUserReplayQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _ArticleUserReplayCache = db.ArticleUserReplays.AsNoTracking().Where(a=>!a.ArticleUserReplay_DelLock && a.ArticleUserReplay_ParentId == "").ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<ArticleUserReplay> GetAll()
        {
            return _ArticleUserReplayCache.OrderBy(t => t.ArticleUserReplay_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<ArticleUserReplay> GetPagedList(int pageNumber, int pageSize)
        {
            return _ArticleUserReplayCache.OrderBy(a => a.ArticleUserReplay_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static ArticleUserReplay Get(string id)
        {
            return _ArticleUserReplayCache.FirstOrDefault(p => p.ArticleUserReplay_Id == id);
        }

        //新增單一記錄
        public static void Create(ArticleUserReplay ArticleUserReplay)
        {
            Create(new List<ArticleUserReplay>() { ArticleUserReplay });
        }

        //新增多筆記錄
        public static void Create(List<ArticleUserReplay> ArticleUserReplays)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_ArticleUserReplayQueueLock)
                {
                    db.ArticleUserReplays.AddRange(ArticleUserReplays);
                    db.SaveChanges();
                    //更新記憶体
                    _ArticleUserReplayCache.AddRange(ArticleUserReplays);
                }
            }
        }

        //更新一筆記錄
        public static void Update(ArticleUserReplay ArticleUserReplays)
        {
            Update(new List<ArticleUserReplay>() { ArticleUserReplays });
        }

        //更新多筆記錄
        public static void Update(List<ArticleUserReplay> ArticleUserReplays)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = ArticleUserReplays.Select(a => a.ArticleUserReplay_Id).ToList();
                var objInDB = db.ArticleUserReplays.Where(a => objIDs.Contains(a.ArticleUserReplay_Id)).ToList();

                foreach (ArticleUserReplay item in objInDB)
                {
                    var theNewFromOutside = ArticleUserReplays.FirstOrDefault(a => a.ArticleUserReplay_Id == item.ArticleUserReplay_Id);

                    Mapper.Map(theNewFromOutside, item);

                }

                lock (_ArticleUserReplayQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _ArticleUserReplayCache.RemoveAll(a => objIDs.Contains(a.ArticleUserReplay_Id));
                    _ArticleUserReplayCache.AddRange(ArticleUserReplays);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(ArticleUserReplay ArticleUserReplay)
        {
            Remove(new List<ArticleUserReplay>() { ArticleUserReplay });
        }

        //刪除多筆記錄
        public static void Remove(List<ArticleUserReplay> ArticleUserReplays)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = ArticleUserReplays.Select(a => a.ArticleUserReplay_Id).ToList();
                var objInDB = db.ArticleUserReplays.Where(a => objIDs.Contains(a.ArticleUserReplay_Id)).ToList();

                lock (_ArticleUserReplayQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    _ArticleUserReplayCache.RemoveAll(a => objIDs.Contains(a.ArticleUserReplay_Id));
                }
            }
        }
        #endregion

        #region Domain & ViewModel 映射

        public static ArticleUserReplayModel DomainToModel(ArticleUserReplay ArticleUserReplay)
        {
            ArticleUserReplayModel viewModel = new ArticleUserReplayModel();
            Mapper.CreateMap<ArticleUserReplay, ArticleUserReplayModel>();
            viewModel = Mapper.Map<ArticleUserReplay, ArticleUserReplayModel>(ArticleUserReplay);

            return viewModel;
        }

        public static ArticleUserReplay ModelToDomain(ArticleUserReplayModel viewModel)
        {
            ArticleUserReplay ArticleUserReplay = new ArticleUserReplay();

            try
            {
                Mapper.CreateMap<ArticleUserReplayModel, ArticleUserReplay>();
                ArticleUserReplay = Mapper.Map<ArticleUserReplayModel, ArticleUserReplay>(viewModel);
            }
            catch (Exception e)
            {

            }


            return ArticleUserReplay;
        }


        public static List<ArticleUserReplayModel> ListDomainToListModel(List<ArticleUserReplay> ArticleUserReplay)
        {
            Mapper.CreateMap<List<ArticleUserReplay>, List<ArticleUserReplayModel>>();
            List<ArticleUserReplayModel> viewModel = Mapper.Map<List<ArticleUserReplay>, List<ArticleUserReplayModel>>(ArticleUserReplay);
            return viewModel;
        }



        #endregion

        #region 進階查詢
        public static List<ArticleUserReplay> getReplayByArticleId(string articleId)
        {
           return _ArticleUserReplayCache.Where(a => a.ArticleUserReplay_FK_ArticleId == articleId).ToList();
        }
        public static List<ArticleUserReplay> getChildReplayId(string id)
        {
            return _ArticleUserReplayCache.Where(a => a.ArticleUserReplay_ParentId == id).ToList();
        }
        #endregion
    }
}