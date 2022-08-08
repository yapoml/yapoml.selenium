using Yapoml.Framework.Options;
using Yapoml.Selenium.Options;
using Yapoml.Selenium.Services.Finder;

namespace Yapoml.Selenium
{
    public static class FinderExtensions
    {
        /// <summary>
        /// Automatically await components to be presented on a page.
        /// Default timeouts can be configured via <see cref="TimeoutExtensions.WithTimeout(ISpaceOptions, System.TimeSpan?, System.TimeSpan?)"/> method.
        /// </summary>
        /// <param name="spaceOptions"></param>
        /// <returns></returns>
        public static ISpaceOptions UseAwaitableComponents(this ISpaceOptions spaceOptions)
        {
            var timeoutOptions = spaceOptions.Services.Get<TimeoutOptions>();

            spaceOptions.Services.Register<IElementFinder>(new AwaitableElementFinder(timeoutOptions));

            return spaceOptions;
        }
    }
}
