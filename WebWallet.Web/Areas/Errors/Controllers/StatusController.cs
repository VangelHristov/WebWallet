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
                case 400: statusmessage = "Заявката не може да се изпълни"; break;
                case 403: statusmessage = "Нямате необходимите привилигии"; break;
                case 404: statusmessage = "Няма такава страница"; break;
                case 408: statusmessage = "Изпълнението на заявката се забави"; break;
                case 500: statusmessage = "Грешка на сървъра. Моля да ни извините"; break;
                default: statusmessage = "Г Р Е Ш К А"; break;
            }

            var viewModel = new StatusCodeVM { Code = statusCode.ToString(), Message = statusmessage };
            return View("/Areas/Errors/Views/StatusCodeView.cshtml", viewModel);
        }
    }
}