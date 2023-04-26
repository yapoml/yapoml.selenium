using Yapoml.Framework.Options;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium
{
    /// <summary>
    /// Various extensions to register base url.
    /// </summary>
    public static class BaseUrlExtensions
    {
        /// <summary>
        /// Registers base url for pages navigation.
        /// <example>
        /// Usage:
        /// <code>
        ///     driver.Ya(opts => opts.WithBaseUrl("https://nuget.org"))
        ///         .HomePage.Open();
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="spaceOptions"></param>
        /// <param name="url">The url to be registered as a base.</param>
        /// <returns></returns>
        public static ISpaceOptions WithBaseUrl(this ISpaceOptions spaceOptions, string url)
        {
            spaceOptions.Services.Register(new BaseUrlOptions(url));

            return spaceOptions;
        }
    }
}
