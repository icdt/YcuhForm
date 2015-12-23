using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class PointCategoryModel
    {
        [Key]
        public string PointCategory_Id { get; set; }

        public string PointCategory_Name { get; set; }

        public bool PointCategory_DelLock { get; set; }

        public DateTime PointCategory_CreateTime { get; set; }

        public DateTime PointCategory_UpdateTime { get; set; }

        public string Article_FK_UserId { get; set; }
        public string Article_FK_UpdateUserId { get; set; }

        public PointCategoryModel()
        {
            PointCategory_Id = Guid.NewGuid().ToString();
            PointCategory_Name = string.Empty;
            PointCategory_CreateTime = DateTime.Now;
            PointCategory_UpdateTime = DateTime.Now;
            PointCategory_DelLock = false;
        }
    }
}