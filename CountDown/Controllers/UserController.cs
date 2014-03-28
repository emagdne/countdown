using System.Web.Mvc;

namespace CountDown.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

//        public ActionResult Index()
//        {
//            return View();
//        }

        public ActionResult Register()
        {
            return View("Registration");
        }

        public ActionResult Login()
        {
            return View("Login");
        }
    }
}
