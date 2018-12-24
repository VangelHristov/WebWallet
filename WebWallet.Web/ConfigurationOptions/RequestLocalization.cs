using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;

namespace WebWallet.Web.ConfigurationOptions
{
    public static class RequestLocalization
    {
        public static RequestLocalizationOptions BulgarianCulture
        {
            get
            {
                var defaultDateCulture = "bg-BG";
                var bulgarianCulture = new CultureInfo(defaultDateCulture);
                bulgarianCulture.NumberFormat.NumberDecimalSeparator = ",";
                bulgarianCulture.NumberFormat.CurrencyDecimalSeparator = ",";

                var cultures = new List<CultureInfo> { bulgarianCulture };
                var requestCulture = new RequestCulture(bulgarianCulture);

                var requestLocalicationOptions = new RequestLocalizationOptions();
                requestLocalicationOptions.DefaultRequestCulture = requestCulture;
                requestLocalicationOptions.SupportedCultures = cultures;
                requestLocalicationOptions.SupportedUICultures = cultures;
                requestLocalicationOptions.DefaultRequestCulture = requestCulture;

                return requestLocalicationOptions;
            }
        }
    }
}