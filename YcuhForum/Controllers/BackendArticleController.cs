using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class BackendArticleController : Controller
    {

        // GET: BackendArticle
        public ActionResult Index(int page = 1 )
        {
          
            return View(ArticleManager.GetPagedList(page, 10));
        }


     
        public string Create()
        {
            TempData["isEdit"] = false;
            ArticleModel model = new ArticleModel();
            InitialViewModel(ref model);
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "_Create",model);
            //ArticleModel model = new ArticleModel();
            //InitialViewModel(ref model);
           // return PartialView("_Create",model);
        }

        // POST: BackendArticle/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleModel articleModel,List<string> userIdList)
        {
            try
            {
                #region 文章
                Article articleObj = ArticleManager.ModelToDomain(articleModel);
                NewCreateModel(ref articleObj);
                ArticleManager.Create(articleObj);
                #endregion

                #region 指定觀看
                List<ArticleUserRecord> articleUserRecord = new List<ArticleUserRecord>();
                for (int i = 0; i < userIdList.Count; i++)
			    {
                    ArticleUserRecord tempArticleUserRecord = new ArticleUserRecord();
                    tempArticleUserRecord.ArticleUserRecord_Id = Guid.NewGuid().ToString();
                    tempArticleUserRecord.ArticleUserRecord_ArticleGroup = articleObj.Article_Group;
                    tempArticleUserRecord.ArticleUserRecord_ArticleTitle = articleObj.Article_Title;
                    tempArticleUserRecord.ArticleUserRecord_ArticleCategory = articleObj.Article_Category;
                    tempArticleUserRecord.ArticleUserRecord_CreateTime = DateTime.Now;
                    tempArticleUserRecord.ArticleUserRecord_UpdateTime = new DateTime();
                    tempArticleUserRecord.ArticleUserRecord_FK_ArticleId = articleObj.Article_Id;
                    tempArticleUserRecord.ArticleUserRecord_FK_UserId = userIdList[i];
                    articleUserRecord.Add(tempArticleUserRecord);
			    }
                ArticleUserRecordManager.Create(articleUserRecord);
                
                #endregion
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: BackendArticle/Edit/5
        public string Edit(string id)
        {
            TempData["isEdit"] = true;
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "_Create", ArticleManager.DomainToModel(ArticleManager.Get(id)));

        }

        // POST: BackendArticle/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleModel articleModel, List<string> userIdList)
        {
            try
            {
                #region 文章修改
                var articleObj = ArticleManager.ModelToDomain(articleModel);
                ArticleManager.Update(articleObj);
                #endregion

                #region 修改指定觀看(若已觀看則不處理)
                List<ArticleUserRecord> articleUserRecord;
                var userEnforceList = ArticleUserRecordManager.getEnforceUser(articleObj.Article_Id);
                articleUserRecord = userEnforceList.Where(a => userIdList.Any(b => a.ArticleUserRecord_FK_UserId == b) && a.ArticleUserRecord_UpdateTime == new DateTime()).ToList();
                ArticleUserRecordManager.Remove(articleUserRecord);
                #endregion

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

       
        // POST: BackendArticle/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                ArticleManager.Remove(ArticleManager.Get(id));

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }


        private void InitialViewModel(ref ArticleModel articleModel)
        {
            articleModel.Article_Id = Guid.NewGuid().ToString();
           
        }
        private void NewCreateModel(ref Article article)
        {
            article.Article_CreateTime = DateTime.Now;
            article.Article_UpdateTime = DateTime.Now;
        }

    }
}
