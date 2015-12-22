using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class BackendPointCategoryController : Controller
    {
        // GET: PointCategory
        public ActionResult Index()
        {
            return View(PointCategoryManager.GetAll());
        }


        public string Create()
        {
            TempData["isEdit"] = false;
            PointCategoryModel model = new PointCategoryModel();
            InitialViewModel(ref model);
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "_Create", model);
        }

        // POST: PointCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PointCategoryModel pointCategory)
        {
            try
            {
                PointCategory PointCategoryObj = PointCategoryManager.ModelToDomain(pointCategory);
                NewCreateModel(ref PointCategoryObj);
                PointCategoryManager.Create(PointCategoryObj);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public string Edit(string id)
        {
            TempData["isEdit"] = true;
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "_Create", PointCategoryManager.DomainToModel(PointCategoryManager.Get(id)));

        }

        // POST: PointCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(PointCategoryModel pointCategoryModel)
        {
            try
            {
                var pointCategoryObj = PointCategoryManager.ModelToDomain(pointCategoryModel);
                PointCategoryManager.Update(pointCategoryObj);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: PointCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                PointCategoryManager.Remove(PointCategoryManager.Get(id));

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return View();
            }
        }



        private void InitialViewModel(ref PointCategoryModel pointCategoryModel)
        {
            pointCategoryModel.PointCategory_Id = Guid.NewGuid().ToString();

        }
        private void NewCreateModel(ref PointCategory pointCategory)
        {
            pointCategory.PointCategory_CreateTime = DateTime.Now;
            pointCategory.PointCategory_UpdateTime = DateTime.Now;
        }
    }
}
