using OpenQA.Selenium;
using System.Collections.Generic;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium.Services.Finder
{
    public class AwaitableElementFinder : IElementFinder
    {
        private readonly TimeoutOptions _timeoutOptions;

        public AwaitableElementFinder(TimeoutOptions timeoutOptions)
        {
            _timeoutOptions = timeoutOptions;
        }

        public IWebElement FindElement(ISearchContext searchContext, By by)
        {
            return Waiter.Until(() =>
            {
                try
                {
                    return searchContext.FindElement(by);
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }, _timeoutOptions.Timeout, _timeoutOptions.PollingInterval);
        }

        public IReadOnlyList<IWebElement> FindElements(ISearchContext searchContext, By by)
        {
            return Waiter.Until(() =>
            {
                try
                {
                    var elements = searchContext.FindElements(by);
                    
                    return elements.Count > 0 ? elements : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }, _timeoutOptions.Timeout, _timeoutOptions.PollingInterval);
        }
    }
}
