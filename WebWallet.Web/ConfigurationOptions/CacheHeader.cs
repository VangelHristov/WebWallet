using Marvin.Cache.Headers;

namespace WebWallet.Web.ConfigurationOptions
{
    public static class CacheHeader
    {
        public static void ExpirationOptions(ExpirationModelOptions expirationModelOptions)
        {
            expirationModelOptions.MaxAge = 1200;
            expirationModelOptions.SharedMaxAge = 1200;
        }

        public static void ValidationOptions(ValidationModelOptions validationModelOptions)
        {
            validationModelOptions.MustRevalidate = true;
            validationModelOptions.ProxyRevalidate = true;
        }
    }
}