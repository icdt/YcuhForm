using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    /// <summary>
    /// 會員點數
    /// </summary>
    public class UserPoint
    {
        [Key]
        public string UserPoint_Id { get; set; }

        public int UserPoint_Point { get; set; }

        public DateTime UserPoint_CreateTime { get; set; }

        public DateTime UserPoint_UpdateTime { get; set; }

        public string UserPoint_FK_UserId { get; set; }
    }
}