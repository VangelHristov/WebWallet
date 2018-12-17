using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

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

        protected void AddModelErrors(ModelStateDictionary modelState)
        {
            modelState
                .Values
                .SelectMany(x => x.Errors)
                .ToList()
                .ForEach(x => modelState.AddModelError("", x.ErrorMessage));
        }
    }
}