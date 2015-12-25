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
        public string Create(string id)
        {
            #region 取出題目
            var examObj = ExaminationManager.GetExaminationByArticleId(id);
            #endregion

            #region 檢查題目狀態
            ViewBag.ArticleId = id;
            if (examObj == null)
            {
                var examQuestionObj = new List<ExamQuestion>();
                return Helper.RenderPartialTool.RenderPartialViewToString(this, "模板", examQuestionObj);
            }
            else
            {
                var examQuestionObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ExamQuestion>(examObj.Examination_Question);
                return Helper.RenderPartialTool.RenderPartialViewToString(this, "模板", examQuestionObj);
            }
            #endregion
        }
        [HttpPost]
        public ActionResult Create(string id, HttpPostedFileBase file)
        {
            #region 取出題目
            var examObj = ExaminationManager.GetExaminationByArticleId(id);
            #endregion
            #region
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


            return RedirectToAction("", "");

        }

    }
}
