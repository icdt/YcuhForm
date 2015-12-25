using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Models;
using YcuhForum.Helper;

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
            ViewBag.Action = "Create";            
            ArticleModel model = new ArticleModel();
            ViewBag.PointCategorySelector = PreparePointCategorySelectList(null);
            ViewBag.ArticleGroupSelector = PrepareArticleGroupSelectList(null);
           
            return RenderPartialTool.RenderPartialViewToString(this, "_Create",model);
          
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
                //更新時間
                articleModel.Article_CreateTime = DateTime.Now;
                articleModel.Article_UpdateTime = DateTime.Now;
                Article articleObj = ArticleManager.ModelToDomain(articleModel);
                ArticleManager.Create(articleObj);
                #endregion

                #region 指定觀看
                SetEnforce(articleObj.Article_Id, userIdList);
                #endregion
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "文章新增異常";
                var actionStr = Newtonsoft.Json.JsonConvert.SerializeObject(articleModel);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                ErrorTool.RecordByDB(newErrorRecord);
                return RedirectToAction("Index");
            }
        }

        // GET: BackendArticle/Edit/5
        public string Edit(string id)
        {
            ViewBag.Action = "Edit";
            var targetObj =  ArticleManager.DomainToModel(ArticleManager.Get(id));
            ViewBag.PointCategorySelector = PreparePointCategorySelectList(targetObj.Article_FK_PointCategoryId);
            ViewBag.ArticleGroupSelector = PrepareArticleGroupSelectList(targetObj.Article_FK_ArticleGroupId);
           return Helper.RenderPartialTool.RenderPartialViewToString(this, "_Create", targetObj);

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
                UpdateEnforce(articleObj.Article_Id, userIdList);
                #endregion

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "文章修改異常";
                var actionStr = Newtonsoft.Json.JsonConvert.SerializeObject(articleModel);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                ErrorTool.RecordByDB(newErrorRecord);
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

        #region 下拉選單
        private List<SelectListItem> PreparePointCategorySelectList(string selectedPointCategoryId)
        {
            var pointCategory = PointCategoryManager.GetAll();
            List<SelectListItem> pointSeletor = new List<SelectListItem>();
            foreach (var item in pointCategory)
            {
                pointSeletor.Add(new SelectListItem()
                {
                    Text = item.PointCategory_Name,
                    Value = item.PointCategory_Id.ToString(),
                    Selected = (item.PointCategory_Id == selectedPointCategoryId)
                });
            }
            return pointSeletor;
        }

        private List<SelectListItem> PrepareArticleGroupSelectList(string selectedArticleGroupId)
        {
            var articleGroup = ArticleGroupManager.GetAll();
            List<SelectListItem> articleGroupSeletor = new List<SelectListItem>();
            foreach (var item in articleGroup)
            {
                articleGroupSeletor.Add(new SelectListItem()
                {
                    Text = item.ArticleGroup_Name,
                    Value = item.ArticleGroup_Id.ToString(),
                    Selected = (item.ArticleGroup_Id == selectedArticleGroupId)
                });
            }
            return articleGroupSeletor;
        }
        #endregion


        #region 觀看者指定修改

        private void SetEnforce(string articleId, List<string> userIdList)
        {
            try
            {
                List<ArticleUserRecord> articleUserRecord = new List<ArticleUserRecord>();
                for (int i = 0; i < userIdList.Count; i++)
                {
                    ArticleUserRecord tempArticleUserRecord = new ArticleUserRecord();
                    tempArticleUserRecord.ArticleUserRecord_Id = Guid.NewGuid().ToString();
                    tempArticleUserRecord.ArticleUserRecord_CreateTime = DateTime.Now;
                    tempArticleUserRecord.ArticleUserRecord_UpdateTime = new DateTime();
                    tempArticleUserRecord.ArticleUserRecord_FK_ArticleId = articleId;
                    tempArticleUserRecord.ArticleUserRecord_FK_UserId = userIdList[i];
                    articleUserRecord.Add(tempArticleUserRecord);
                }
                ArticleUserRecordManager.Create(articleUserRecord);
            }
            catch (Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "指定觀看新增異常";
                var userStr = Newtonsoft.Json.JsonConvert.SerializeObject(userIdList);
                var actionStr = "目標文章:" + articleId + "目標資料:" + userStr;
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                ErrorTool.RecordByDB(newErrorRecord);
            }
          
        }

        private void UpdateEnforce(string articleId, List<string> userIdList)
        {
            try
            {
                List<ArticleUserRecord> articleUserRecordRemoveList = new List<ArticleUserRecord>();
                List<ArticleUserRecord> articleUserRecordCreteList = new List<ArticleUserRecord>();
                var userEnforceList = ArticleUserRecordManager.getEnforceUser(articleId);
                //有比對到就刪除,沒有就新增
                foreach (var item in userIdList)
                {
                    var targetObj = userEnforceList.Where(a => userIdList.Any(b => b == a.ArticleUserRecord_FK_UserId)).FirstOrDefault();
                    if (targetObj == null)
                    {
                        ArticleUserRecord newArticleUserRecord = new ArticleUserRecord();
                        newArticleUserRecord.ArticleUserRecord_FK_ArticleId = articleId;
                        newArticleUserRecord.ArticleUserRecord_FK_UserId = item;
                        newArticleUserRecord.ArticleUserRecord_CreateTime = DateTime.Now;
                        newArticleUserRecord.ArticleUserRecord_UpdateTime = DateTime.Now;
                        newArticleUserRecord.ArticleUserRecord_IsEnforce = true;
                        articleUserRecordCreteList.Add(newArticleUserRecord);
                    }
                    else
                    {
                        articleUserRecordRemoveList.Add(targetObj);
                    }
                }
            }
            catch (Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "指定觀看修改異常";
                var userStr = Newtonsoft.Json.JsonConvert.SerializeObject(userIdList);
                var actionStr = "目標文章:" + articleId + "目標資料:" + userStr;
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                ErrorTool.RecordByDB(newErrorRecord);
            }


        }
        #endregion

    }
}
