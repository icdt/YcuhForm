using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class AuthOption
    {
        [Key]
        public string AuthOption_Id { get; set; }

        public string AuthOption_Name { get; set; }

        public bool AuthOption_DelLock { get; set; }

        public DateTime AuthOption_CreateTime { get; set; }

        public DateTime AuthOption_UpdateTime { get; set; }

        public string AuthOption_FK_UpdateUserId { get; set; }

        public string AuthOption_FK_UserId { get; set; }
    }
}