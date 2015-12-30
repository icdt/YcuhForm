using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using YcuhForum.Models;

namespace YcuhForum.Controllers
{
    public class BaseController : Controller
    {
        private List<string> _groupList = new List<string>();

        private string _strUserId;
        private bool _backendSpAuth;

        /// <summary>
        /// 客戶前端顯示面板群組
        /// </summary>
        public List<string> groupList
        {
            get
            {
                var userObj = AUManager.GetUserByUserName(User.Identity.Name);
                if (userObj == null)
                {
                    _groupList = new List<string>();
                }
                else
                {
                    var authOption =  Newtonsoft.Json.JsonConvert.DeserializeObject<List<AuthOption>>(userObj.ApplicationUser_AuthOption);
                    foreach (var item in authOption)
	                {
                        _groupList.Add(item.AuthOption_Id);
		 
	                }
                }
                return _groupList;
            }
        }

        /// <summary>
        /// 客戶Id
        /// </summary>
        public string strUserId
        {
            get
            {
                var userObj = AUManager.GetUserByUserName(User.Identity.Name);
                if (userObj == null)
                {
                    _strUserId = "";
                }
                else
                {
                    _strUserId = userObj.Id;
                }
                return _strUserId;
            }
            set
            {
                _strUserId = value;
            }
        }

        /// <summary>
        /// 可否進後台
        /// </summary>
        public bool backendSpAuth
        {
            get
            {
                var userObj = AUManager.GetUserByUserName(User.Identity.Name);
                if (userObj == null)
                {
                    _backendSpAuth = false;
                }
                else
                {
                    _backendSpAuth = userObj.ApplicationUser_BackendAuth;
                }
                return _backendSpAuth;
            }
            set
            {
                _backendSpAuth = value;
            }
        }
    }
}
