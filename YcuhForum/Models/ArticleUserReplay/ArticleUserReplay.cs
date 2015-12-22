using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ArticleUserReplay
    {
        [Key]
        public string ArticleUserReplay_Id { get; set; }

        public string ArticleUserReplay_Group { get; set; }

        public string ArticleUserReplay_Category { get; set; }

        public string ArticleUserReplay_Title { get; set; }

        public string ArticleUserReplay_Content { get; set; }
        
        public bool ArticleUserReplay_DelLock { get; set; }
        
        public DateTime ArticleUserReplay_CreateTime { get; set; }

        public DateTime ArticleUserReplay_UpdateTime { get; set; }

        public string ArticleUserReplay_FK_UserId { get; set; }

        public string ArticleUserReplay_FK_ArticleId { get; set; }
    }
}