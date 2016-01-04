using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YcuhForum.Models
{
    public class ICDTAuthorize : AuthorizeAttribute
    {

        private string[] _authOptions { get; set; }

        public ICDTAuthorize(string authOption)
        {
            _authOptions = authOption.Split(';');
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            try
            {
                if (AuthorizeCore(filterContext.HttpContext))
                {
                    HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                    cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                    cachePolicy.AddValidationCallback(new HttpCacheValidateHandler(this.CacheValidateHandler), null);
                }
                else
                {
                    HandleUnauthorizedRequest(filterContext);
                }
            }
            catch (Exception)
            {
                HandleUnauthorizedRequest(filterContext);
            }
          
        }

     
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            var tempUserObj = httpContext.User;
            var userObj = AUManager.GetUserByUserName(tempUserObj.Identity.Name);
            if (userObj == null)
            {
                throw new Exception();
            }
            var authOptionList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AuthOption>>(userObj.ApplicationUser_AuthOption);
            if (authOptionList.Any(a => _authOptions.Any(b=>b == a.AuthOption_Name)))
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        protected void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }



        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            filterContext.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = "ErrorPage", action = "Index" }));
        }
    }
}