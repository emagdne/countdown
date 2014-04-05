using System.Web.Mvc;

namespace CountDown.Controllers
{
    public class ToDoController : Controller
    {
        public ActionResult Create()
        {
            return View("Create");
        }
    }
}
