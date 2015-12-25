using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class AuthOptionModel
    {
        public string AuthOption_Id { get; set; }

        public string AuthOption_Name { get; set; }

        public bool AuthOption_DelLock { get; set; }

        public DateTime AuthOption_CreateTime { get; set; }

        public DateTime AuthOption_UpdateTime { get; set; }

        public string AuthOption_FK_UpdateUserId { get; set; }

        public string AuthOption_FK_UserId { get; set; }


        public AuthOptionModel()
        {
            AuthOption_Id = Guid.NewGuid().ToString();
            AuthOption_DelLock = false;
            AuthOption_CreateTime = DateTime.Now;
            AuthOption_UpdateTime = DateTime.Now;

        }
    }


}