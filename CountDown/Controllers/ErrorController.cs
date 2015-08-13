using System.Web.Mvc;

namespace CountDown.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}