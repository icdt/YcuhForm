using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult List()
        {
            return View();
        }

        public ActionResult Content()
        {
            return View();
        }

        public ActionResult Reply()
        {
            return PartialView("_Reply");
        }

        public ActionResult Test()
        {
            return PartialView("_Test");
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ArticleModel model = new ArticleModel();
            ViewBag.PointCategorySelector = PreparePointCategorySelectList(null);
            //ViewBag.ArticleGroupSelector = PrepareUserList();

            return View(model);
        }

        /// <summary>
        /// 編輯文章
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            ViewBag.PointCategorySelector = PreparePointCategorySelectList(null);
            //ViewBag.ArticleGroupSelector = PrepareUserList();
            return View();
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

        private MultiSelectList PrepareUserList()
        {
            var userObjList = AUManager.GetAll();

            return new MultiSelectList(userObjList, "Id", "ApplicationUser_Name", new List<string>());
        }
        #endregion
    }
}