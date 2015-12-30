using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class BackendArticleGroupController : BaseController
    {
        public ActionResult Index()
        {
            return View(ArticleGroupManager.GetAll());
        }

        //AJAX
        public string Create()
        {
            ViewBag.Action = "Create";
            ArticleGroupModel model = new ArticleGroupModel();
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "_Create", model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleGroupModel articleGroupModel)
        {
            try
            {
                ArticleGroup articleGroupObj = ArticleGroupManager.ModelToDomain(articleGroupModel);
                ArticleGroupManager.Create(articleGroupObj);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "文章群組新增異常";
                var actionStr = "建立者:" + strUserId + ";" + Newtonsoft.Json.JsonConvert.SerializeObject(articleGroupModel);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
                TempData["ErrorMesage"] = "新增失敗";
                return RedirectToAction("Index");
            }
        }

        //AJAX
        public string Edit(string id)
        {
            ViewBag.Action = "Edit";
            var targetObj = ArticleGroupManager.DomainToModel(ArticleGroupManager.Get(id));
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "_Create", targetObj);

        }


        [HttpPost]
        public ActionResult Edit(ArticleGroupModel articleGroupModel)
        {
            try
            {
                var articleGroupObj = ArticleGroupManager.ModelToDomain(articleGroupModel);
                ArticleGroupManager.Update(articleGroupObj);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "文章群組修改異常";
                var actionStr = "修改者:" + strUserId + ";" + Newtonsoft.Json.JsonConvert.SerializeObject(articleGroupModel);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
                TempData["ErrorMesage"] = "修改失敗";
                return RedirectToAction("Index");
            }
        }
    
    }
}
