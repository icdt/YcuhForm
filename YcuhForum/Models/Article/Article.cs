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
       
        public string Article_Title { get; set; }

        public string Article_Content { get; set; }

        public int Article_Point { get; set; }

        public string Article_FileUrl { get; set; }

        public bool Article_IsShow { get; set; }

        public bool Article_IsReply { get; set; }

        public string Article_OtherSiteUrl { get; set; }

        public int Article_Count { get; set; }

        public bool Article_DelLock { get; set; }

        public DateTime Article_CreateTime { get; set; }

        public DateTime Article_UpdateTime { get; set; }

        public string Article_FK_UpdateUserId { get; set; }

        public string Article_FK_UserId { get; set; }

        public string Article_FK_ArticleGroupId { get; set; }

        public string Article_FK_PointCategoryId { get; set; }

        
        public Article()
        {
            Article_Id = Guid.NewGuid().ToString();
            Article_Title = string.Empty;
            Article_Content = string.Empty;
            Article_FileUrl = string.Empty;
            Article_OtherSiteUrl = string.Empty;
            Article_IsShow = true;
            Article_IsReply = true;
            Article_CreateTime = DateTime.Now;
            Article_UpdateTime = DateTime.Now;
            Article_Count = 0;
            Article_Point = 0;
            Article_FK_ArticleGroupId = string.Empty;
            Article_FK_PointCategoryId = string.Empty;
        }
   
    }

}