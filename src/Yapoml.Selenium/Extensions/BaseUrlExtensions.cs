using Yapoml.Framework.Options;

namespace Yapoml.Selenium
{
    public static class BaseUrlExtensions
    {
        public static ISpaceOptions UseBaseUrl(this ISpaceOptions spaceOptions, string url)
        {
            spaceOptions.Services.Register(new BaseUrlOptions(url));

            return spaceOptions;
        }
    }

    public class BaseUrlOptions
    {
        public BaseUrlOptions(string baseUrl)
        {
            Url = baseUrl;
        }

        public string Url { get; }
    }
}
