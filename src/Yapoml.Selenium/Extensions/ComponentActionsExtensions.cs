using Yapoml.Framework.Options;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium
{
    /// <summary>
    /// Provides a posssibility to set default behavior for actions.
    /// </summary>
    public static class ComponentActionsExtensions
    {
        public static ISpaceOptions WithScrollIntoViewOptions(this ISpaceOptions spaceOptions, ScrollIntoViewOptions options)
        {
            spaceOptions.Services.Register(options);

            return spaceOptions;
        }
    }
}
