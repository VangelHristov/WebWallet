using Microsoft.AspNetCore.Mvc;
using WebWallet.Web.ModelBinders;

namespace WebWallet.Web.ConfigurationOptions
{
    public static class Mvc
    {
        public static void Options(MvcOptions mvc)
        {
            mvc.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            mvc.ModelBinderProviders.Insert(0, new InvariantDecimalModelBinderProvider());
        }
    }
}