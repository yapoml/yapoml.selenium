using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using Yapoml.Selenium.Components.Conditions.Generic;

namespace Yapoml.Selenium.Components.Conditions
{
    /// <summary>
    /// Various conditions for awaiting url.
    /// </summary>
    /// <typeparam name="TConditions">Fluent original instance for chaining conditions.</typeparam>
    public class UrlConditions<TConditions> : StringConditions<TConditions>
    {
        public UrlConditions(IWebDriver webDriver, TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {
            _fetchFunc = () => webDriver.Url;
        }

        protected override Exception GetIsError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url doesn't equal to '{expectedValue}'.", innerException);
        }

        protected override Exception GetIsNotError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url doesn't equal to '{expectedValue}'.", innerException);
        }

        protected override Exception GetStartsWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url doesn't start with '{expectedValue}'.", innerException);
        }

        protected override Exception GetDoesNotStartWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url starts with '{expectedValue}'.", innerException);
        }

        protected override Exception GetEndsWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url doesn't end with '{expectedValue}'.", innerException);
        }

        protected override Exception GetDoesNotEndWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url ends with '{expectedValue}'.", innerException);
        }

        protected override Exception GetContainsError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url doesn't contain '{expectedValue}' yet.", innerException);
        }

        protected override Exception GetDoesNotContainError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url contains '{expectedValue}'.", innerException);
        }

        protected override Exception GetMatchesError(string latestValue, Regex regex, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url doesn't match '{regex}'.", innerException);
        }

        protected override Exception GetDoesNotMatchError(string latestValue, Regex regex, Exception innerException)
        {
            return new TimeoutException($"'{latestValue}' url matches '{regex}'.", innerException);
        }
    }
}
