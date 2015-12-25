using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ErrorRecord
    {
        [Key]
        public string ErrorRecord_Id { get; set; }

        public string ErrorRecord_SystemMessage { get; set; }

        public string ErrorRecord_ActionDescribe { get; set; }

        public string ErrorRecord_CustomedMessage { get; set; }
        public DateTime ErrorRecord_CreateTime { get; set; }


        public ErrorRecord()
        {
            ErrorRecord_Id = Guid.NewGuid().ToString();
            ErrorRecord_CreateTime = DateTime.Now;
        }

          
    }
}