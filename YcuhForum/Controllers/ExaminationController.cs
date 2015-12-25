using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class ExaminationController : BaseController
    {
      //AJAX
        public string Detail(string articleId)
        {
            #region 取出題目
            var examObj = ExaminationManager.GetExaminationByArticleId(articleId);
            var examModelObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExamQuestion>>(examObj.Examination_Question);
            GetRandom(ref examModelObj);
            #endregion

            #region 榜定考試券
            List<ExamQuestionModel> newExamQuestionModel = new  List<ExamQuestionModel>();
            Mapper.CreateMap<List<ExamQuestion>,List<ExamQuestionModel>>();
            newExamQuestionModel = Mapper.Map<List<ExamQuestion>,List<ExamQuestionModel>>(examModelObj);
            #endregion
            
            #region 回傳物件
            ViewBag.ArticleId = articleId;
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "模板", examModelObj);
            #endregion
        }




        [HttpPost]
        public string Create(List<ExamQuestionModel> examQuestion, string articleId)
        {
            #region 取出題目(含解答)
            var examObj = ExaminationManager.GetExaminationByArticleId(articleId);
            var examAnserObj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExamQuestionModel>>(examObj.Examination_Question);
            #endregion

            #region 答案比對
                ExaminationRecord newExaminationRecord = new ExaminationRecord();
                newExaminationRecord.ExaminationRecord_Id = Guid.NewGuid().ToString();
                newExaminationRecord.ExaminationRecord_ArticleId = articleId;
                newExaminationRecord.ExaminationRecord_UserId = strUserId;
                newExaminationRecord.ExaminationRecord_CreateTime = DateTime.Now;
                newExaminationRecord.ExaminationRecord_UpdateTime = DateTime.Now;

                foreach (var item in examAnserObj)
                {
                    //客戶作答題目
                    var tempOption = examQuestion.Where(a => a.Id == item.Id).FirstOrDefault();
                    //客戶作答題目選項與對應題目選項(含正確解答)相比
                    for (int k = 0; k < tempOption.Options.Count; k++)
                    {
                        //當兩邊為T時即為正解,其他一律為F
                        if ((tempOption.Options[k].CustomerAnswer == item.Options[k].Answer) == true)
                        {
                            newExaminationRecord.ExaminationRecord_CorrentNumber += 1;
                            tempOption.Options[k].Answer = true;
                        }
                        else
                        {
                            newExaminationRecord.ExaminationRecord_ErrorNumber += 1;
                            tempOption.Options[k].Answer = item.Options[k].Answer;
                        }
                    }
                }
            #endregion

            #region 回傳物件

            ViewBag.AnserSheet = true;
            return Helper.RenderPartialTool.RenderPartialViewToString(this, "模板", examQuestion);
           
            #endregion
        }


        private void GetRandom(ref List<ExamQuestion> examQuestionList)
        {
            foreach (var item in examQuestionList)
            {
                item.Options = item.Options.OrderBy(a => Guid.NewGuid()).ToList();
            }
            examQuestionList.OrderBy(a => Guid.NewGuid());
        }

    }
}
