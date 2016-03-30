using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace YcuhForum.Controllers
{
    public class BackendCkeditorController : BaseController
    {
        /// <summary>
        /// 編輯器圖片上傳
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="CKEditorFuncNum"></param>
        /// <param name="CKEditor"></param>
        /// <param name="langCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Uploadp(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string result = "";
            if (upload != null && upload.ContentLength > 0)
            {
                //儲存圖片至Server
                upload.SaveAs(Server.MapPath("~/Upload/image/" + upload.FileName));

                var imageUrl = Url.Content("~/Upload/image/" + upload.FileName);
                var vMessage = string.Empty;

                result = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + imageUrl + "\", \"" + vMessage + "\");</script></body></html>";

            }

            return Content(result);
        }

    }
}