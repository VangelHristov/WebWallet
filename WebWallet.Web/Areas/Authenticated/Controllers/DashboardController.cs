using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Web.Controllers;

namespace WebWallet.Web.Areas.Dashboard.Controllers
{
    [Authorize]
    [Area("Authenticated")]
    public class DashboardController : BaseController
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}