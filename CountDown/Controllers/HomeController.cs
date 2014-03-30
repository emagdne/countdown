using System.Web.Mvc;

namespace CountDown.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            /* 
             * todo: If user is logged in, forward request to ToDoController->List
             * Else, show index page
             */
            return View("Index");
        }

    }
}
