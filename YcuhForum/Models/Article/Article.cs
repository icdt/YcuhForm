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

        public string Article_FK_UserId { get; set; }
       
        public string Article_FK_GroupId { get; set; }

        public string Article_FK_PointCategoryId { get; set; }

        public string Article_Title { get; set; }

        public string Article_Content { get; set; }

        public int Article_Point { get; set; }

        public string Article_FileUrl { get; set; }

        public bool Article_IsShow { get; set; }

        public bool Article_IsReply { get; set; }

        public string Article_OtherSiteUrl { get; set; }

        public bool Article_DelLock { get; set; }

        public DateTime Article_CreateTime { get; set; }

        public DateTime Article_UpdateTime { get; set; }

        public string Article_FK_UpdateUserId { get; set; }

        //補建構子
    }

}