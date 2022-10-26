using OpenQA.Selenium;
using System.Collections.Generic;
using System.Diagnostics;

namespace Yapoml.Selenium.Services.Locator
{
    public class DefaultElementLocator : IElementLocator
    {
        [DebuggerHidden]
        public IWebElement FindElement(string componentFriendlyName, ISearchContext searchContext, By by)
        {
            return searchContext.FindElement(by);
        }

        [DebuggerHidden]
        public IReadOnlyList<IWebElement> FindElements(ISearchContext searchContext, By by)
        {
            return searchContext.FindElements(by);
        }
    }
}
