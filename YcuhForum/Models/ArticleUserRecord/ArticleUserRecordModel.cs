﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcuhForum.Models
{
    public class ArticleUserRecordModel
    {
        public string ArticleUserRecord_Id { get; set; }

        public string ArticleUserRecord_ArticleGroup { get; set; }

        public string ArticleUserRecord_ArticleCategory { get; set; }

        public string ArticleUserRecord_ArticleTitle { get; set; }

        public DateTime ArticleUserRecord_CreateTime { get; set; }

        public DateTime ArticleUserRecord_UpdateTime { get; set; }

        public bool ArticleUserRecord_DelLock { get; set; }

        public string ArticleUserRecord_FK_UserId { get; set; }

        public string ArticleUserRecord_FK_ArticleId { get; set; }
    }
}