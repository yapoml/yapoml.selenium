using Yapoml.Framework.Options;

namespace Yapoml.Selenium
{
    public static class BaseUrlExtensions
    {
        public static ISpaceOptions UseBaseUrl(this ISpaceOptions spaceOptions, string url)
        {
            spaceOptions.Services.Register(new BaseUrl(url));

            return spaceOptions;
        }
    }

    public class BaseUrl
    {
        public BaseUrl(string baseUrl)
        {
            Url = baseUrl;
        }

        public string Url { get; }
    }
}
