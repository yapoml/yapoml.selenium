using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [DebuggerHidden]
        public IWebElement FindElement(ISearchContext searchContext, By by)
        {
            Exception lastError = null;

            var ignoredExceptions = new Dictionary<Type, uint>
            {
                { typeof(NoSuchElementException), 0}
            };

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

                        ignoredExceptions[ex.GetType()]++;

                        return null;
                    }
                }, _timeoutOptions.Timeout, _timeoutOptions.PollingInterval);
            }
            catch (TimeoutException)
            {
                throw Waiter.BuildTimeoutException($"Component is not located yet '{by}'.", lastError, _timeoutOptions.Timeout, _timeoutOptions.PollingInterval, ignoredExceptions);
            }
        }

        [DebuggerHidden]
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
