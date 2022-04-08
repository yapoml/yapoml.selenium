using Yapoml.Options;

namespace Yapoml.Selenium
{
    public static class BaseUrlExtensions
    {
        public static ISpaceOptions UseBaseUrl(this ISpaceOptions spaceOptions, string url)
        {
            spaceOptions.Register(new BaseUrl(url));

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
