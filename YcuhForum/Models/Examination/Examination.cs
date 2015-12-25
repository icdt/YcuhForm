using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    /// <summary>
    /// 考試物件
    /// </summary>
    public class Examination
    {
        [Key]
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


        public Examination()
        {
            Examination_Id = Guid.NewGuid().ToString();
            Examination_Question = string.Empty;
            Examination_FileUrl = string.Empty;
            Examination_QuestionNumber = 0;
            Examination_CreateTime = DateTime.Now;
            Examination_UpdateTime = DateTime.Now;
        }

    }
}