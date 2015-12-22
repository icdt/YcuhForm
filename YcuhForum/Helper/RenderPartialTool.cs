using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YcuhForum.Helper
{
    public static class RenderPartialTool
    {

        public static string RenderPartialViewToString(dynamic target, string viewName, object model)
        {
            target.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(target.ControllerContext, viewName);
                var viewContext = new ViewContext(target.ControllerContext, viewResult.View, target.ViewData, target.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}