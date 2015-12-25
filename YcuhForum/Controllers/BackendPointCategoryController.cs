using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
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
            ViewBag.Action = false;
            PointCategoryModel model = new PointCategoryModel();
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
                PointCategoryManager.Create(PointCategoryObj);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "點數群組新增異常";
                var actionStr = Newtonsoft.Json.JsonConvert.SerializeObject(pointCategory);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public string Edit(string id)
        {
            ViewBag.Action = true;
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
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "點數群組修改異常";
                var actionStr = Newtonsoft.Json.JsonConvert.SerializeObject(pointCategoryModel);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
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


    }
}
