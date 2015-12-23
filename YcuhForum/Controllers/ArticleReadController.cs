using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class ArticleReadController : BaseController
    {
        //列表
        public ActionResult Index()
        {
            var tempData = ArticleManager.getArticleByUser(groupList);
            return View(tempData.GroupBy(a => a.Article_FK_ArticleGroupId));
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
            catch (Exception)
            {
                //存log
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(bool userChecked)
        {
            try
            {
                if (userChecked)
                {
                  
                }
            }
            catch (Exception)
            {
                //存log
            }

            return RedirectToAction("Index");
        }

        // GET: ArticleRead/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArticleRead/Edit/5
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

        // GET: ArticleRead/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArticleRead/Delete/5
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


        #region 

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
    }
}
