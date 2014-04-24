using System;
using System.Web.Mvc;
using System.Web.Security;
using CountDown.Models.Domain;
using CountDown.Models.Repository;
using CountDown.Models.Service;

namespace CountDown.Controllers
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 4/10/14</para>
    /// </summary>
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public UserController()
        {
            _userRepository = new UserRepository();
            _authenticationService = new AuthenticationService();
        }

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _authenticationService = new AuthenticationService();
        }

        public UserController(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
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
                    TempData["loginMessage"] = "You have successfully registered for the application.\n" +
                                               "Use the form below to login.";
                    TempData["registeredEmail"] = user.Email;
                    return RedirectToAction("Login");
                }
                catch (Exception)
                {
                    return View("SystemError");
                }
            }

            return View("Registration");
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                if (User != null && User.Identity.IsAuthenticated)
                {
                    TempData["loginMessage"] = "You have signed out of the application successfully.";
                    FormsAuthentication.SignOut();
                }
                return View("Login");
            }
            catch (Exception)
            {
                return View("SystemError");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginAttempt attempt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_authenticationService.ValidateUser(attempt.Email, attempt.Password))
                    {
                        _authenticationService.HandleLoginRedirect(attempt.Email, false);
                    }
                    else
                    {
                        TempData["loginError"] = "The email address or password could not be verified.\n" +
                                                 "Please enter a registered email and password.";
                    }
                }
                else
                {
                    TempData["loginError"] = "You must correct the errors below to login.";
                }
                return View("Login");
            }
            catch (Exception)
            {
                return View("SystemError");
            }
        }
    }
}