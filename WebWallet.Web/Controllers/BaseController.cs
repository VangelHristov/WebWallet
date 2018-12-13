using Microsoft.AspNetCore.Mvc;
using System;

namespace WebWallet.Web.Controllers
{
    public class BaseController : Controller
    {
        protected void ThrowIfNull(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}