using OpenQA.Selenium;
using System.Collections.Generic;

namespace Yapoml.Selenium.Services.Locator
{
    public interface IElementLocator
    {
        IWebElement FindElement(ISearchContext searchContext, By by);

        IReadOnlyList<IWebElement> FindElements(ISearchContext searchContext, By by);
    }
}
