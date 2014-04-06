using System.Web.Mvc;

namespace CountDown.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("Index");
            }
            TempData["loginMessage"] = "Please login or register to use the application.";
            return RedirectToAction("Login", "User");
        }
    }
}