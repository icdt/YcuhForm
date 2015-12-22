using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace YcuhForum.Controllers
{
    public class BaseController : Controller
    {
        private List<string> _groupList;

        private string _strUserId;



        /// <summary>
        /// 客戶前端顯示面板群組
        /// </summary>
        public List<string> groupList
        {
           //未決
            get
            {
                return _groupList;
            }
        }


        public string strUserId
        {
            //未決
            get
            {
                return _strUserId;
            }
        }
    }
}
