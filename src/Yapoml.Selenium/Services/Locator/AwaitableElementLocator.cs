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

        public IWebElement FindElement(string componentFriendlyName, ISearchContext searchContext, By by)
        {
            Exception lastError = null;

            try
            {
                return Waiter.Until(() =>
                {
                    try
                    {
                        return searchContext.FindElement(by);
                    }
                    catch (NoSuchElementException ex)
                    {
                        lastError = ex;

                        return null;
                    }
                }, _timeoutOptions.Timeout, _timeoutOptions.PollingInterval);
            }
            catch(TimeoutException)
            {
                throw Waiter.BuildTimeoutException($"{componentFriendlyName} component is not located yet '{by}'.", lastError, _timeoutOptions.Timeout, _timeoutOptions.PollingInterval, new List<Type> { typeof(NoSuchElementException) });
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
