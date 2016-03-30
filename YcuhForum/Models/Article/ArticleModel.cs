using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YcuhForum.Models
{
    public class ArticleModel
    {
        public string Article_Id { get; set; }

        [Required]
        [Display(Name = "文章標題")]
        public string Article_Title { get; set; }

        [Display(Name = "文章內容")]
        public string Article_Content { get; set; }

        public int Article_Point { get; set; }

        //[StringLength(10,MinimumLength=5)]
        //[Remote("CheckObj", "Validate", ErrorMessage = "遠端驗證失敗")]
        public string Article_FileUrl { get; set; }

        [Display(Name = "是否公開/發布")]
        public bool Article_IsShow { get; set; }

        [Display(Name = "是否開放回覆")]
        public bool Article_IsReply { get; set; }

        [Display(Name = "外部連結")]
        public string Article_OtherSiteUrl { get; set; }

        public bool Article_DelLock { get; set; }

        public DateTime Article_CreateTime { get; set; }

        public DateTime Article_UpdateTime { get; set; }

        public string Article_FK_UpdateUserId { get; set; }

        public string Article_FK_UserId { get; set; }

        public string Article_FK_ArticleGroupId { get; set; }

        [Display(Name = "文章類型")]
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