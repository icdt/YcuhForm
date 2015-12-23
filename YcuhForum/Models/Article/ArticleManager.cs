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
                _ArticleCache = db.Articles.AsNoTracking().Where(a=>!a.Article_DelLock).ToList();
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
        public static void Create(Article article)
        {
            Create(new List<Article>() { article });
        }

        //新增多筆記錄
        public static void Create(List<Article> articles)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                lock (_ArticleQueueLock)
                {
                    db.Articles.AddRange(articles);
                    db.SaveChanges();
                    //更新記憶体
                    _ArticleCache.AddRange(articles);
                }
            }
        }

        //更新一筆記錄
        public static void Update(Article articles)
        {
            Update(new List<Article>() { articles });
        }

        //更新多筆記錄
        public static void Update(List<Article> articles)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = articles.Select(a => a.Article_Id).ToList();
                var objInDB = db.Articles.Where(a => objIDs.Contains(a.Article_Id)).ToList();

                foreach (Article item in objInDB)
                {
                    var theNewFromOutside = articles.FirstOrDefault(a => a.Article_Id == item.Article_Id);

                    item.Article_FK_PointCategoryId = theNewFromOutside.Article_FK_PointCategoryId;

                    item.Article_Content = theNewFromOutside.Article_Content;

                    item.Article_DelLock = theNewFromOutside.Article_DelLock;

                    item.Article_FileUrl = theNewFromOutside.Article_FileUrl;

                    item.Article_FK_ArticleGroupId = theNewFromOutside.Article_FK_ArticleGroupId;

                    item.Article_IsReply = theNewFromOutside.Article_IsReply;

                    item.Article_IsShow = theNewFromOutside.Article_IsShow;
                    
                    item.Article_OtherSiteUrl = theNewFromOutside.Article_OtherSiteUrl;

                    item.Article_Title = theNewFromOutside.Article_Title;

                    item.Article_UpdateTime = DateTime.Now;


                }

                lock (_ArticleQueueLock)
                {
                    db.SaveChanges();
                   
                    //更新記憶体
                    _ArticleCache.RemoveAll(a => objIDs.Contains(a.Article_Id));
                    _ArticleCache.AddRange(articles);
                }
            }
        }

        //刪除一筆記錄
        public static void Remove(Article article)
        {
            Remove(new List<Article>() { article });
        }

        //刪除多筆記錄
        public static void Remove(List<Article> articles)
        {
            //更新資料庫
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var objIDs = articles.Select(a => a.Article_Id).ToList();
                var objInDB = db.Articles.Where(a => objIDs.Contains(a.Article_Id)).ToList();

                foreach (var item in objInDB)
                {
                    item.Article_DelLock = true;
                }

                lock (_ArticleQueueLock)
                {
                    db.SaveChanges();

                    //更新記憶体
                    foreach (var item in articles)
                    {
                        //有問題須修改
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
            Mapper.CreateMap<Article, ArticleModel>();
            viewModel = Mapper.Map<Article, ArticleModel>(Article);

            return viewModel;
        }

        public static Article ModelToDomain(ArticleModel viewModel)
        {
            Article Article = new Article();

            try
            {
                Mapper.CreateMap<ArticleModel, Article>();
                Article = Mapper.Map<ArticleModel, Article>(viewModel);
            }
            catch(Exception e)
            {

            }
          

            return Article;
        }

        #endregion

        #region 進階查詢
        public static List<Article> getArticleByUser(List<string> groupList)
        {
            return _ArticleCache.Where(a => groupList.Any(b => a.Article_FK_ArticleGroupId == b) && a.Article_IsShow && !a.Article_DelLock).OrderByDescending(a => a.Article_CreateTime).ToList();
        }


        #endregion
    }
}