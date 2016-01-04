using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace YcuhForum.Controllers
{
    public class ValidateController : Controller
    {

        public JsonResult CheckObj(string Article_FileUrl)
        {
            bool isValidate = false;

            if (Url.IsLocalUrl(Request.Url.AbsoluteUri))
            {
                IList<PropertyInfo> propList = Article_FileUrl.GetType().GetProperties();

              foreach (var item in propList)
              {
                  object objData = item.GetValue(this, null);
                   string propertyName = item.Name;
              }
               
            }
            // Remote 驗證是使用 Get 因此要開放
            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }
    }
}