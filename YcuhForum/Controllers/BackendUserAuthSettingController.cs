using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
  
    public class BackendUserAuthSettingController : BaseController
    {
        
        public ActionResult Index()
        {
            var userListObj = AUManager.GetAll();
            var userModelListObj = AUManager.DomainListToModelList(userListObj);
            return View(userModelListObj);
        }


      
        public ActionResult Edit(string id)
        {
            var userObj = AUManager.Get(id);
            ViewBag.AuthOptionSelector = PrepareAuthOptionList();
            return PartialView("模板", userObj);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUserModel userModelObj, List<string> authoptionList)
        {
            try
            {
                var userObj = AUManager.ModelToDomain(userModelObj);
                var authoptionListObj =  AuthOptionManager.GetAuthoptionByIdList(authoptionList);
                userObj.ApplicationUser_AuthOption = Newtonsoft.Json.JsonConvert.SerializeObject(authoptionListObj);
                AUManager.Update(userObj);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "客戶權限設定異常";
                var actionStr = "建立者:" + strUserId + ";" + Newtonsoft.Json.JsonConvert.SerializeObject(userModelObj) +
                                ";目標權限" + Newtonsoft.Json.JsonConvert.SerializeObject(authoptionList);
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
                TempData["ErrorMesage"] = "儲存失敗";
                return RedirectToAction("Index");
            }
        }



        private MultiSelectList PrepareAuthOptionList()
        {
            var authOptionList = AuthOptionManager.GetAll();

            return new MultiSelectList(authOptionList, "Id", "AuthOption_Name", new List<string>());
        }
    }
}
