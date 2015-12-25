using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class BackendAuthOptionController : BaseController
    {
        // GET: BackendAuthOption
        public ActionResult Index()
        {
            var authoptionObj = AuthOptionManager.GetAll();
            var authoptionModelObj = AuthOptionManager.DomainToModel(authoptionObj);
            return View(authoptionModelObj);
        }

        //AJAX
        public string Create()
        {
            ViewBag.Action = "Create";
            AuthOptionModel model = new AuthOptionModel();
            return RenderPartialTool.RenderPartialViewToString(this, "模板", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthOptionModel authoptionModel)
        {
            var authoptionObj = AuthOptionManager.ModelToDomain(authoptionModel);
            authoptionObj.AuthOption_FK_UserId = strUserId;
            authoptionObj.AuthOption_FK_UpdateUserId = strUserId;
            AuthOptionManager.Create(authoptionObj);
            return RedirectToAction("Index");
        }
        //AJAX
        public string Edit(string id)
        {
            ViewBag.Action = "Edit";
            var authoptionObj = AuthOptionManager.Get(id);
            var authoptionModelObj = AuthOptionManager.DomainToModel(authoptionObj);
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "模板", authoptionModelObj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthOptionModel authoptionModel)
        {
            var authoptionObj = AuthOptionManager.ModelToDomain(authoptionModel);
            authoptionObj.AuthOption_FK_UpdateUserId = strUserId;
            AuthOptionManager.Update(authoptionObj);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                AuthOptionManager.Remove(AuthOptionManager.Get(id));

                return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
        }



    }
}
