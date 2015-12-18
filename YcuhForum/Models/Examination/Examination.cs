using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class Examination
    {
        [Key]
        public string Examination_Id { get; set; }

        public string Examination_User { get; set; }

        public string Examination_Amount { get; set; }

        public string Examination_Question { get; set; }

        public string Examination_QuestionNumber { get; set; }

        public string Examination_File { get; set; }

        public DateTime Examination_CreateTime { get; set; }

        public DateTime Examination_UpdateTime { get; set; }
        public string Examination_FK_Article { get; set; }

    }
}