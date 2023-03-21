using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Yapoml.Selenium.Services.Locator;

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

            throw new TimeoutException($"{condition} wasn't met during {timeout.TotalSeconds} seconds and polling each {pollingInterval.TotalSeconds} seconds.");
        }

        public static IWebElement UntilDisplayed(IElementHandler elementHandler, TimeSpan timeout, TimeSpan pollingInterval)
        {
            Exception lastError = null;

            Dictionary<Type, uint> _ignoredExceptions = new Dictionary<Type, uint> {
                { typeof(NoSuchElementException), 0 },
                { typeof(StaleElementReferenceException), 0 }
             };

            IWebElement condition()
            {
                try
                {
                    var element = elementHandler.Locate();

                    if (element.Displayed)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex) when (_ignoredExceptions.ContainsKey(ex.GetType()))
                {
                    if (ex is StaleElementReferenceException)
                    {
                        elementHandler.Invalidate();
                    }

                    lastError = ex;

                    _ignoredExceptions[ex.GetType()]++;

                    return null;
                }
            }

            try
            {
                return Until(condition, timeout, pollingInterval);
            }
            catch (TimeoutException)
            {
                throw BuildTimeoutException($"{elementHandler.ComponentMetadata.Name} component is not displayed yet '{elementHandler.By}'.", lastError, timeout, pollingInterval, _ignoredExceptions);
            }
        }

        public static void UntilEnabled(IElementHandler elementHandler, TimeSpan timeout, TimeSpan pollingInterval)
        {
            bool? condition()
            {
                if (elementHandler.Locate().Enabled)
                {
                    return true;
                }
                else
                {
                    return null;
                }
            }

            try
            {
                Until(condition, timeout, pollingInterval);
            }
            catch (TimeoutException)
            {
                throw BuildTimeoutException($"{elementHandler.ComponentMetadata.Name} component is not enabled yet.", null, timeout, pollingInterval, null);
            }
        }

        public static void UntilCssValue(IElementHandler elementHandler, string propertyName, string expectedValue, TimeSpan timeout, TimeSpan pollingInterval)
        {
            string latestValue = null;

            bool? condition()
            {
                latestValue = elementHandler.Locate().GetCssValue(propertyName);

                if (expectedValue == latestValue)
                {
                    return true;
                }
                else
                {
                    return null;
                }
            }

            try
            {
                Until(condition, timeout, pollingInterval);
            }
            catch (TimeoutException)
            {
                throw BuildTimeoutException($"CSS '{propertyName} = {latestValue}' of the {elementHandler.ComponentMetadata.Name} component is not '{expectedValue}' yet.", null, timeout, pollingInterval, null);
            }
        }

        public static void UntilAttributeValue(IElementHandler elementHandler, string attributeName, string expectedValue, TimeSpan timeout, TimeSpan pollingInterval)
        {
            string latestValue = null;

            bool? condition()
            {
                latestValue = elementHandler.Locate().GetAttribute(attributeName);

                if (expectedValue == latestValue)
                {
                    return true;
                }
                else
                {
                    return null;
                }
            }

            try
            {
                Until(condition, timeout, pollingInterval);
            }
            catch (TimeoutException)
            {
                throw BuildTimeoutException($"Attribute '{attributeName} = {latestValue}' of the {elementHandler.ComponentMetadata.Name} component is not '{expectedValue}' yet.", null, timeout, pollingInterval, null);
            }
        }

        public static TimeoutException BuildTimeoutException(string message, Exception innerException, TimeSpan timeout, TimeSpan pollingInterval, IDictionary<Type, uint> ignoredExceptionTypes)
        {
            var builder = new StringBuilder();

            builder.AppendLine(message);

            builder.AppendLine();

            builder.AppendLine($"  Timeout is {timeout.TotalSeconds} seconds and polling each {pollingInterval.TotalSeconds} seconds.");

            builder.AppendLine();

            if (ignoredExceptionTypes != null)
            {
                builder.AppendLine("  Ignored exceptions:");

                foreach (var ignoredExceptionType in ignoredExceptionTypes)
                {
                    builder.AppendLine($"   - {ignoredExceptionType.Key.FullName} ({ignoredExceptionType.Value} times)");
                }
            }

            return new TimeoutException(builder.ToString(), innerException);
        }
    }
}
