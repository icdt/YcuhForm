using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{

    #region 上資料
    public class ExamQuestion
    {
        public int Id { get; set; }

        public String Question { get; set; }

        public List<OptionsAndAnswer> Options { get; set; }

    }
    public class OptionsAndAnswer
    {
        public string Option { get; set; }

        public Boolean Answer { get; set; }
    }
    #endregion



    #region 前台考試
    public class ExamQuestionModel
    {
        public int Id { get; set; }

        public String Question { get; set; }

        public List<OptionsAndAnswerModel> Options { get; set; }

    }
    public class OptionsAndAnswerModel
    {
        public string Option { get; set; }

        public Boolean Answer { get; set; }

        public Boolean CustomerAnswer { get; set; }

    }
    #endregion

 

}