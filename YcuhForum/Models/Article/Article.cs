using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class Article
    {
        [Key]
        public string Article_Id { get; set; }

        public string Article_Creater { get; set; }

        public string Article_Group { get; set; }

        public string Article_Category { get; set; }

        public string Article_Title { get; set; }

        public string Article_Name { get; set; }

        public string Article_Content { get; set; }

        public string Article_File { get; set; }

        public string Article_IsShow { get; set; }

        public string Article_IsReplay { get; set; }

        public string Article_OtherSiteUrl { get; set; }

        public bool Article_DelLock { get; set; }

        public DateTime Article_CreateTime { get; set; }

        public DateTime Article_UpdateTime { get; set; }

        public string Article_FK_Point { get; set; }
    }
}