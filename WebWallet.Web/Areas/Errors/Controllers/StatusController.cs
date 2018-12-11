using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebWallet.ViewModels;

namespace WebWallet.Web.Areas.Errors.Controllers
{
    public class StatusController : Controller
    {
        [Route("/StatusCode/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var statusmessage = string.Empty;

            switch (statusCode)
            {
                case 400: statusmessage = "Bad request: The request cannot be fulfilled due to bad syntax"; break;
                case 403: statusmessage = "Forbidden"; break;
                case 404: statusmessage = "Page not found"; break;
                case 408: statusmessage = "The server timed out waiting for the request"; break;
                case 500: statusmessage = "Internal Server Error - server was unable to finish processing the request"; break;
                default: statusmessage = "That’s odd... Something we didn't expect happened"; break;
            }

            var viewModel = new StatusCodeVM { Code = statusCode.ToString(), Message = statusmessage };
            return View("/Areas/Errors/Views/StatusCodeView.cshtml", viewModel);
        }
    }
}