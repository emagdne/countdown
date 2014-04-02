using System;
using System.Web.Mvc;
using CountDown.Models.Domain;
using CountDown.Models.Repository;

namespace CountDown.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult Register()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.InsertUser(user);
                    _userRepository.SaveChanges();
                    return RedirectToAction("Login");
                }
                catch (Exception)
                {
                    return View("SystemError");
                }
            }

            return View("Registration");
        }

        public ActionResult Login()
        {
            return View("Login");
        }
    }
}
