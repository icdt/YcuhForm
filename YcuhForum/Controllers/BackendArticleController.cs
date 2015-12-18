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


        [ChildActionOnly]
        public PartialViewResult Create()
        {
            ArticleModel model = new ArticleModel();
            InitialViewModel(ref model);
            return PartialView("_Create",model);
        }

        // POST: BackendArticle/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BackendArticle/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BackendArticle/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BackendArticle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BackendArticle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        private void InitialViewModel(ref ArticleModel articleModel)
        {
            articleModel.Article_Id = Guid.NewGuid().ToString();
        }
    }
}
