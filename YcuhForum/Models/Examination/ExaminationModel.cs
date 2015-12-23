using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ExaminationModel
    {
        public string Examination_Id { get; set; }

        public string Examination_TestedUserIdList { get; set; }

        public string Examination_Question { get; set; }

        public int Examination_QuestionNumber { get; set; }

        public string Examination_FileUrl { get; set; }

        public bool Examination_DelLock { get; set; }

        public DateTime Examination_CreateTime { get; set; }

        public DateTime Examination_UpdateTime { get; set; }

        public string Examination_FK_UserId { get; set; }

        public string Examination_FK_UpdateUserId { get; set; }

        public string Examination_FK_ArticleId { get; set; }

        public ExaminationModel()
        {
            Examination_Id = Guid.NewGuid().ToString();
            Examination_Question = string.Empty;
            Examination_FileUrl = string.Empty;
            Examination_QuestionNumber = 0;
            Examination_CreateTime = DateTime.Now;
            Examination_UpdateTime = DateTime.Now;
        }
    }






    public class ExamOptionModel
    {
        public String Question { get; set; }

        public List<OptionsAndAnswerModel> Options { get; set; }

    }
    public class OptionsAndAnswerModel
    {
        public string Option { get; set; }

        public Boolean Answer { get; set; }
    }
}