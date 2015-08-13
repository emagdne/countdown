using System.Web.Mvc;
using CountDown.Models.Security;

namespace CountDown.Controllers
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public class ApplicationController : Controller
    {
        private const string DefaultErrorMessage = "An unexpected error occurred while processing your request.";

        protected CountDownIdentity AuthenticatedUser
        {
            get { return User != null ? User.Identity as CountDownIdentity : null; }
        }

        protected bool IsUserAuthenticated()
        {
            return User != null && User.Identity.IsAuthenticated;
        }

        protected JsonResult JsonSuccessResponse()
        {
            return Json(new {Status = "Success"});
        }

        protected JsonResult JsonSuccessResponse(object data)
        {
            return Json(new {Status = "Success", Data = data});
        }

        protected JsonResult JsonErrorResponse()
        {
            return Json(new {Status = "Error", Error = DefaultErrorMessage});
        }

        protected JsonResult JsonErrorResponse(string error)
        {
            return Json(new {Status = "Error", Error = error});
        }

        protected JsonResult JsonErrorResponse(string error, object data)
        {
            return Json(new {Status = "Error", Error = error, Data = data});
        }
    }
}