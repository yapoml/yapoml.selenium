using OpenQA.Selenium;
using System.Collections.Generic;

namespace Yapoml.Selenium.Services.Locator
{
    public class DefaultElementLocator : IElementLocator
    {
        public IWebElement FindElement(ISearchContext searchContext, By by)
        {
            return searchContext.FindElement(by);
        }

        public IReadOnlyList<IWebElement> FindElements(ISearchContext searchContext, By by)
        {
            return searchContext.FindElements(by);
        }
    }
}
