using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class ForumController : BaseController
    {
        //首頁載具
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult LaatedArticle()
        {
            var articleObjList = ArticleManager.getArticleByTime();
            var viewData = ArticleManager.DomainToModel(articleObjList);
            return PartialView("模板", viewData);
        }

        [ChildActionOnly]
        public PartialViewResult GroupArticle()
        {
            var articleObjList = ArticleManager.getArticleByGroup();

            return PartialView("模板", articleObjList);
        }


     
    }
}
