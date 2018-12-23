using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebWallet.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly List<SelectListItem> _periods = new List<SelectListItem>
            {
                new SelectListItem { Value = "7 дни", Text = "7 дни" },
                new SelectListItem { Value = "14 дни", Text = "14 дни" },
                new SelectListItem { Value = "1 месец", Text = "1 месец"  },
                new SelectListItem { Value = "3 месеца", Text = "3 месеца" },
                new SelectListItem { Value = "6 месеца", Text = "6 месеца"  },
                new SelectListItem { Value = "1 година", Text = "1 година"  }
            };

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