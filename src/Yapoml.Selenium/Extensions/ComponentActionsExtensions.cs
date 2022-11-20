using Yapoml.Framework.Options;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium
{
    /// <summary>
    /// Provides a possibility to set default behavior for actions.
    /// </summary>
    public static class ComponentActionsExtensions
    {
        public static ISpaceOptions WithScrollIntoViewOptions(this ISpaceOptions spaceOptions, ScrollIntoViewOptions options)
        {
            spaceOptions.Services.Register(options);

            return spaceOptions;
        }

        public static ISpaceOptions WithFocusOptions(this ISpaceOptions spaceOptions, FocusOptions options)
        {
            spaceOptions.Services.Register(options);

            return spaceOptions;
        }
    }


}
