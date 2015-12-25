using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class ArticleReplayController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult GetView(string id)
        {
            var catchData = ArticleUserReplayManager.getReplayByArticleId(id);
            var viewData = ArticleUserReplayManager.ListDomainToListModel(catchData);
            //要丟回來的物件
            var newArticleUserReplayModel = new ArticleUserReplayModel();
            TempData["newArticleUserReplayModel"] = newArticleUserReplayModel;
            return PartialView("列表模板", viewData);
        }

        //AJAX 來回
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleUserReplayModel articleUserReplay)
        {
            try
            {
                var newData = ArticleUserReplayManager.ModelToDomain(articleUserReplay);
                ArticleUserReplayManager.Create(newData);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "回覆文章異常";
                var actionStr = Newtonsoft.Json.JsonConvert.SerializeObject(articleUserReplay);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
               
                ErrorTool.RecordByDB(newErrorRecord);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

    }
}
