using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ArticleManager
    {
        private static List<Article> _ArticleCache = new List<Article>();
        private static object _ArticleQueueLock = new Object();

        #region 初始化
        public static void Initial()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                _ArticleCache = db.Articles.ToList();
            }
        }
        #endregion

        #region 基本操作 (db & cache)
        //取得所有記錄
        public static List<Article> GetAll()
        {
            return _ArticleCache.OrderBy(t => t.Article_CreateTime).ToList();
        }

        //分頁
        public static IPagedList<Article> GetPagedList(int pageNumber, int pageSize)
        {
            return _ArticleCache.OrderBy(a => a.Article_CreateTime).ToPagedList(pageNumber, pageSize);
        }

        //透過Id取得記錄
        public static Article Get(string id)
        {
            return _ArticleCache.FirstOrDefault(p => p.Article_Id == id);
        }

        //新增單一記錄
        public static void Create(Article Article)
        {
            Create(new List<Article>() { Article });
        }

        //新增多筆記錄
        public static void Create(List<Article> Articles)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_ArticleQueueLock)
                {
                    db.Articles.AddRange(Articles);
                    db.SaveChanges();
                    //更新記憶体
                    _ArticleCache.AddRange(Articles);
                }
            }
        }

        //更新一筆記錄
        public static void Update(Article Articles)
        {
            Update(new List<Article>() { Articles });
        }

        //更新多筆記錄
        public static void Update(List<Article> Articles)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = Articles.Select(a => a.Article_Id).ToList();
                var objInDB = db.Articles.Where(a => objIDs.Contains(a.Article_Id)).ToList();

                foreach (Article item in objInDB)
                {
                    var theNewFromOutside = Articles.FirstOrDefault(a => a.Article_Id == item.Article_Id);

                    Mapper.Map(theNewFromOutside, item);

                }

                lock (_ArticleQueueLock)
                {
                    db.SaveChanges();

                    foreach (var item in Articles)
                    {
                        _ArticleCache.Remove(item);
                    }
                    //更新記憶体
                    _ArticleCache.AddRange(Articles);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(Article Article)
        {
            Remove(new List<Article>() { Article });
        }

        //刪除多筆記錄
        public static void Remove(List<Article> Articles)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = Articles.Select(a => a.Article_Id).ToList();
                var objInDB = db.Articles.Where(a => objIDs.Contains(a.Article_Id)).ToList();

                foreach (var item in objInDB)
                {
                    item.Article_DelLock = true;
                }

                lock (_ArticleQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    foreach (var item in Articles)
                    {
                        _ArticleCache.Remove(item);
                    }
                }
            }
        }
        #endregion

        #region Domain & ViewModel 映射

        public static ArticleModel DomainToModel(Article Article)
        {
            ArticleModel viewModel = new ArticleModel();

            viewModel = Mapper.Map<Article, ArticleModel>(Article);

            return viewModel;
        }

        public static Article ModelToDomain(ArticleModel viewModel)
        {
            Article Article = new Article();

            Article = Mapper.Map<ArticleModel, Article>(viewModel);

            return Article;
        }

        #endregion

        #region 進階查詢

        #endregion
    }
}