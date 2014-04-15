using System.Web.Mvc;

namespace CountDown.Controllers
{
    public class ApplicationController : Controller
    {
        private const string DefaultErrorMessage = "An unexpected error occurred while processing your request.";

        protected JsonResult JsonSuccessResponse()
        {
            return Json(new { Status = "Success" });            
        }

        protected JsonResult JsonSuccessResponse(object data)
        {
            return Json(new {Status = "Success", Data = data});
        }

        protected JsonResult JsonErrorResponse()
        {
            return Json(new { Status = "Error", Error = DefaultErrorMessage });            
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