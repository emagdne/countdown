using System.Web.Mvc;
using System.Web.Routing;

namespace CountDown
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
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
            routes.MapRoute("CompleteToDo", "todo/complete", new {controller = "ToDo", action = "Complete"});
            routes.MapRoute("EditToDo", "todo/edit", new {controller = "ToDo", action = "Edit"});
            routes.MapRoute("UpdateToDo", "todo/update", new {controller = "ToDo", action = "Update"});
            routes.MapRoute("EditToDoCancel", "todo/edit/cancel", new {controller = "ToDo", action = "CancelEdit"});
            routes.MapRoute("DeleteToDo", "todo/delete", new {controller = "ToDo", action = "Delete"});
            routes.MapRoute("Error", "error", new {controller = "Error", action = "Index"});
        }
    }
}