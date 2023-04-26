using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium
{
    public static class BaseUrlExtensions
    {
        public static ISpaceOptions WithBaseUrl(this ISpaceOptions spaceOptions, string url)
        {
            spaceOptions.Services.Register(new BaseUrlOptions(url));

            return spaceOptions;
        }

        public static ISpaceOptions WithBaseUrl(this ISpaceOptions spaceOptions, Uri url)
        {
            return spaceOptions.WithBaseUrl(url.ToString());
        }
    }
}
