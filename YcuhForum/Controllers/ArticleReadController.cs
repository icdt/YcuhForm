using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class ArticleReadController : BaseController
    {
        //列表
        public ActionResult Index(string id, int page = 1)
        {
            var tempData = ArticleManager.getArticleByGroupId(id, page, 10);
            ViewBag.SortCategory = PrepareSortPointCategory();
            return View(tempData);
        }

        //單篇文章      
        public ActionResult Detail(string id)
        {
            try
            {
                var catcheData = ArticleManager.Get(id);
                var viewData = ArticleManager.DomainToModel(catcheData);
                GetUserJobTitleAndArticleGroupNameAndPoingCatgoryName(ref viewData);
                return View(viewData);
            }
            catch (Exception e)
            {
                //存log
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "讀取文章內容異常";
                var actionStr = id;
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
            }

            return RedirectToAction("Index");
        }


        #region guid轉中文

        private void GetUserJobTitleAndArticleGroupNameAndPoingCatgoryName(ref ArticleModel articleModel)
        {
            //職務
            var userObj = AUManager.Get(articleModel.Article_FK_UserId);
            articleModel.Article_UserJobTitle = userObj.ApplicationUser_Job;

            //群組
            var articleGroup = ArticleGroupManager.Get(articleModel.Article_FK_ArticleGroupId);
            articleModel.Article_ArticleGroupName = articleGroup.ArticleGroup_Name;

            //點數
            var pointCategory = PointCategoryManager.Get(articleModel.Article_FK_PointCategoryId);
            articleModel.Article_PointCategoryName = pointCategory.PointCategory_Name;
        }


       
        #endregion

        #region 過濾物件
        private List<SelectListItem> PrepareSortPointCategory()
        {
            var pointCategory = PointCategoryManager.GetAll();
            List<SelectListItem> categorySeletor = new List<SelectListItem>();
            foreach (var item in pointCategory)
            {
                categorySeletor.Add(new SelectListItem()
                {
                    Text = item.PointCategory_Name,
                    Value = item.PointCategory_Id.ToString()
                });
            }
            return categorySeletor;
        }
        #endregion
    }
}
