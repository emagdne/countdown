using System.Web.Mvc;
using System.Web.Routing;

namespace CountDown
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.MapRoute("Home", "", new {controller = "Home", action = "Index"});
            routes.MapRoute("Login", "login", new {controller = "User", action = "Login"});
            routes.MapRoute("Register", "register", new {controller = "User", action = "Register"});
            routes.MapRoute("CreateToDo", "todo/create", new {controller = "ToDo", action = "Create"});
            routes.MapRoute("CompleteToDo", "todo/complete", new { controller = "ToDo", action = "Complete" });
//            routes.MapRoute(
//                name: "Default",
//                url: "{controller}/{action}/{id}",
//                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
//            );
        }
    }
}