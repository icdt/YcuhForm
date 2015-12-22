using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    /// <summary>
    /// 考試記錄
    /// </summary>
    public class ExaminationRecord
    {
        [Key]
        public string ExaminationRecord_Id { get; set; }

        public string ExaminationRecord_ArticleGroup { get; set; }

        public string ExaminationRecord_ArticleCategory { get; set; }

        public string ExaminationRecord_ArticleTitle { get; set; }

        public string ExaminationRecord_ExaminationQuestionNumber { get; set; }

        public string ExaminationRecord_CorrentNumber { get; set; }

        public string ExaminationRecord_ErrorNumber { get; set; }

        public bool ExaminationRecord_IsPass { get; set; }

        public DateTime ExaminationRecord_CreateTime { get; set; }

        public DateTime ExaminationRecord_UpdateTime { get; set; }

        public string ExaminationRecord_UserId { get; set; }
    }
}