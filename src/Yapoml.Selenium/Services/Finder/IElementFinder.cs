using OpenQA.Selenium;
using System.Collections.Generic;

namespace Yapoml.Selenium.Services.Finder
{
    public interface IElementFinder
    {
        IWebElement FindElement(ISearchContext searchContext, By by);

        IReadOnlyList<IWebElement> FindElements(ISearchContext searchContext, By by);
    }
}
