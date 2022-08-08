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

            while (stopwatch.Elapsed <= timeout)
            {
                var result = condition();
                if (result != null)
                {
                    return result;
                }

                Thread.Sleep(pollingInterval);
            }

            throw new TimeoutException("my message");
        }

        public static IWebElement UntilDisplayed(ISearchContext searchContext, By by, TimeSpan timeout, TimeSpan pollingInterval)
        {
            IWebElement condition()
            {
                try
                {
                    var element = searchContext.FindElement(by);

                    return element.Displayed ? element : null;
                }
                catch (Exception ex) when (ex is NoSuchElementException || ex is StaleElementReferenceException)
                {
                    return null;
                }
            }

            try
            {
                return Until(condition, timeout, pollingInterval);
            }
            catch(TimeoutException)
            {
                throw new TimeoutException($"Element {by} couldn't be displayed during {timeout} timeout.");
            }
        }
    }
}
