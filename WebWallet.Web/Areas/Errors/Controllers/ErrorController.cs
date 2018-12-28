using Microsoft.AspNetCore.Mvc;

namespace WebWallet.Web.Areas.Errors.Controllers
{
    [Area("Errors")]
    public class ErrorController : Controller
    {
        [Route("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var status = HttpContext.Response.StatusCode;
            return Redirect("/StatusCode/500");
        }
    }
}