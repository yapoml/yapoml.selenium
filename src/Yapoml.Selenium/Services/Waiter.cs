using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;

namespace Yapoml.Selenium.Services
{
    public static class Waiter
    {
        public static TResult Until<TResult>(Func<TResult> condition, TimeSpan timeout, TimeSpan pollingInterval)
        {
            var stopwatch = Stopwatch.StartNew();

            Exception lastError = null;

            while (stopwatch.Elapsed <= timeout)
            {
                try
                {
                    var result = condition();
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    lastError = ex;
                }

                Thread.Sleep(pollingInterval);
            }

            throw new TimeoutException("my message", lastError);
        }

        public static IWebElement UntilDisplayed(ISearchContext searchContext, By by, TimeSpan timeout, TimeSpan pollingInterval)
        {
            IWebElement condition()
            {
                var element = searchContext.FindElement(by);

                return element.Displayed ? element : null;
            }

            return Until(condition, timeout, pollingInterval);
        }
    }
}
