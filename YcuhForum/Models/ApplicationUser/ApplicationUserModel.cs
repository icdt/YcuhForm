using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ApplicationUserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        #region 自訂區塊
        public string ApplicationUser_Department { get; set; }

        public string ApplicationUser_Job { get; set; }

        public string ApplicationUser_Gender { get; set; }

        public string ApplicationUser_Tel { get; set; }

        public string ApplicationUser_Address { get; set; }

        public string ApplicationUser_IsActive { get; set; }

        public string ApplicationUser_AuthOption { get; set; }

        public bool ApplicationUser_DelLock { get; set; }

        public DateTime ApplicationUser_CreateTime { get; set; }

        public DateTime ApplicationUser_UpdateTime { get; set; }

        public string ApplicationUser_FK_UpdateUserId { get; set; }
        #endregion
     
    }
}