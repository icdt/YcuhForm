using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ArticleModel
    {
        public string Article_Id { get; set; }

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

        public string Article_FK_UserId { get; set; }

        public string Article_FK_ArticleGroupId { get; set; }

        public string Article_FK_PointCategoryId { get; set; }

        #region 畫面所需其他表部分資料

        public string Article_UserJobTitle { get; set; }

        public string Article_ArticleGroupName { get; set; }

        public string Article_PointCategoryName { get; set; }
        #endregion

        public ArticleModel()
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
        }
    }
}