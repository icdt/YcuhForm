using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var newArticleUserReplayModel = new ArticleUserReplayModel();
            InitModel(ref newArticleUserReplayModel);
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
                newCreateModel(ref articleUserReplay);
                var newData = ArticleUserReplayManager.ModelToDomain(articleUserReplay);
                ArticleUserReplayManager.Create(newData);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }


        private void InitModel(ref ArticleUserReplayModel articleUserReplayModel)
        {
            articleUserReplayModel.ArticleUserReplay_Id = Guid.NewGuid().ToString();
        }

        private void newCreateModel(ref ArticleUserReplayModel articleUserReplayModel)
        {
            articleUserReplayModel.ArticleUserReplay_CreateTime = DateTime.Now;
            articleUserReplayModel.ArticleUserReplay_UpdateTime = DateTime.Now;
        }
    }
}
