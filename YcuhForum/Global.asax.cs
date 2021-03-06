﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YcuhForum.Models;

namespace YcuhForum
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbSeed.SeedManager();
            #region 快取
            ArticleManager.Initial();
            PointCategoryManager.Initial();
            ArticleGroupManager.Initial();
            AUManager.Initial();
            #endregion

        }
        #region 出錯時
        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();

            HttpContext con = HttpContext.Current;

            Response.Clear();
            Response.Redirect("/");

        }
        #endregion
     
    }
}
