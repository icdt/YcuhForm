using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class BackendPointCategoryController : BaseController
    {
        // GET: PointCategory
        public ActionResult Index()
        {
            return View(PointCategoryManager.GetAll());
        }


        public ActionResult Create()
        {
            ViewBag.Action = false;
            PointCategoryModel model = new PointCategoryModel();
            return PartialView("模板", model);
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
                var actionStr = "建立者:" + strUserId + ";" + Newtonsoft.Json.JsonConvert.SerializeObject(pointCategory);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Action = true;
            var pointCategoryObj = PointCategoryManager.Get(id);
            return PartialView("模板", pointCategoryObj);
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
                var actionStr = "修改者:" + strUserId + ";" + Newtonsoft.Json.JsonConvert.SerializeObject(pointCategoryModel);
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
