using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ArticleGroup
    {
        [Key]
        public string ArticleGroup_Id { get; set; }

        public string ArticleGroup_Name { get; set; }

        public bool ArticleGroup_DelLock { get; set; }

        public string ArticleGroup_FK_UserId { get; set; }

        public DateTime ArticleGroup_CreateTime { get; set; }

        public string Article_FK_UpdateUserId { get; set; }

        public DateTime ArticleGroup_UpdateTime { get; set; }

       

        public ArticleGroup()
        {
            ArticleGroup_Id = Guid.NewGuid().ToString();
            ArticleGroup_Name = string.Empty;
            ArticleGroup_CreateTime = DateTime.Now;
            ArticleGroup_UpdateTime = DateTime.Now;
            ArticleGroup_DelLock = false;
        }
    }
}