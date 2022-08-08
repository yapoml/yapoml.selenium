using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium.Services.Locator
{
    public class AwaitableElementLocator : IElementLocator
    {
        private readonly TimeoutOptions _timeoutOptions;

        public AwaitableElementLocator(TimeoutOptions timeoutOptions)
        {
            _timeoutOptions = timeoutOptions;
        }

        public IWebElement FindElement(ISearchContext searchContext, By by)
        {
            try
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
            catch(TimeoutException)
            {
                throw new TimeoutException($"Couldn't find element by {by} in {searchContext} during {_timeoutOptions.Timeout}");
            }
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
