using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class ArticleReplayController : BaseController
    {
        [ChildActionOnly]
        public PartialViewResult GetView(string id)
        {
            //次層
            var catchData = ArticleUserReplayManager.getReplayByArticleId(id);
            var viewData = ArticleUserReplayManager.ListDomainToListModel(catchData);
            //次次層
            foreach (var item in viewData)
	        {
		      item.ArticleUserReplayModelList = ArticleUserReplayManager.ListDomainToListModel(ArticleUserReplayManager.getChildReplayId(item.ArticleUserReplay_Id));
	        }
          
            return PartialView("列表模板", viewData);
        }

        public PartialViewResult Create(string id)
        {
            //要丟回來的物件
            var newArticleUserReplayModel = new ArticleUserReplayModel();
            newArticleUserReplayModel.ArticleUserReplay_ParentId = id;
            return PartialView("回覆模板", newArticleUserReplayModel);
        }

        //AJAX 來回
        [HttpPost]
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
