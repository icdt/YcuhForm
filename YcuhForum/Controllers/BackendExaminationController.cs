using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Helper;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class BackendExaminationController : BaseController
    {
        ///怪怪的
        //AJAX
        public ActionResult Create(string id)
        {
            #region 取出題目
            var examObj = ExaminationManager.GetExaminationByArticleId(id);
            #endregion

            #region 檢查題目狀態
            ViewBag.ArticleId = id;
            if (examObj == null)
            {
                var examQuestionObj = new List<ExamQuestion>();
                return PartialView("模板", examQuestionObj);
            }
            else
            {
                var examQuestionObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ExamQuestion>(examObj.Examination_Question);
                return PartialView("模板", examQuestionObj);
            }
            #endregion
        }
        [HttpPost]
        public ActionResult Create(string id, HttpPostedFileBase file)
        {

            #region 檔案驗證

            if (file == null || file.ContentLength <= 0)
            {
                return RedirectToAction("", "");
            }
            if (file.FileName.ToLower().Substring(file.FileName.LastIndexOf(".") + 1) != "xlsx"
                && file.FileName.ToLower().Substring(file.FileName.LastIndexOf(".") + 1) != "xls")
            {
                return RedirectToAction("", "");
            }
            #endregion


            #region 取出考試物件題目
            var examObj = ExaminationManager.GetExaminationByArticleId(id);
            #endregion

            try
            {
                #region 取出題目
                if (examObj == null)
                {
                    Examination newExamination = new Examination();
                    newExamination.Examination_Question = ExaminationTools.CreateExamination(file);
                    ExaminationManager.Create(newExamination);
                }
                else
                {
                    examObj.Examination_Question = ExaminationTools.CreateExamination(file);
                    examObj.Examination_FK_UpdateUserId = strUserId;
                    ExaminationManager.Update(examObj);
                }
                #endregion
            }
            catch (Exception e)
            {
                ErrorRecord newErrorRecord = new ErrorRecord();
                newErrorRecord.ErrorRecord_SystemMessage = e.Message;
                newErrorRecord.ErrorRecord_ActionDescribe = "上傳題目異常";
                var actionStr = "目標考題:" + id + ";" + "檔案名稱:" + file.FileName + ";";
                newErrorRecord.ErrorRecord_CustomedMessage = actionStr;
                newErrorRecord.ErrorRecord_CreateTime = DateTime.Now;
                ErrorTool.RecordByDB(newErrorRecord);
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            return RedirectToAction("", "");

          

        }

    }
}
