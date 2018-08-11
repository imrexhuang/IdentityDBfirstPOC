using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace IdentityDBfirstPOC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //http://kevintsengtw.blogspot.com/2013/11/aspnet-mvc.html
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            bool hasUser = HttpContext.Current.User != null;
            bool isAuthenticated = hasUser && HttpContext.Current.User.Identity.IsAuthenticated;
            bool isIdentity = isAuthenticated && HttpContext.Current.User.Identity is FormsIdentity;

            if (isIdentity)
            {
                //取得表單認證目前這位使用者的身分
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;

                //取得FormsAuthenticationTicket物件
                FormsAuthenticationTicket ticket = id.Ticket;

                //取得UserData 欄位資料(這裡儲存的是角色), 如果有多個角色可以用逗號分隔
                string[] roles = ticket.UserData.Split(',');

                //賦予該使用者新的身分(含角色資訊)
                HttpContext.Current.User = new GenericPrincipal(id , roles);
            }
        }
    }
}
