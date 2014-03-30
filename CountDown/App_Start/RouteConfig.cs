using System.Web.Mvc;
using System.Web.Routing;

namespace CountDown
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.MapRoute("Home", "countdown", new {controller = "Home", action = "Index"});
            routes.MapRoute("Login", "countdown/login", new {controller = "User", action = "Login"});
            routes.MapRoute("Register", "countdown/register", new {controller = "User", action = "Register"});
            routes.MapRoute("CreateToDo", "countdown/todo/create", new {controller = "ToDo", action = "Create"});
//            routes.MapRoute(
//                name: "Default",
//                url: "{controller}/{action}/{id}",
//                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
//            );
        }
    }
}