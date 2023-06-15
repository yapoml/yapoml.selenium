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
    public class UrlConditions<TConditions> : TextualConditions<TConditions>
    {
        private readonly IWebDriver _webDriver;

        public UrlConditions(IWebDriver webDriver, TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {
            _webDriver = webDriver;
        }

        protected override Func<string> FetchValueFunc => () => _webDriver.Url;

        protected override string GetIsError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' url doesn't equal to '{expectedValue}'.{GetDifference(expectedValue, latestValue)}";
        }

        protected override string GetIsNotError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' url doesn't equal to '{expectedValue}'.{GetDifference(expectedValue, latestValue)}";
        }

        protected override string GetIsEmptyError(string latestValue)
        {
            return $"'{latestValue}' url is not empty yet.";
        }

        protected override string GetIsNotEmptyError(string latestValue)
        {
            return $"'{latestValue}' url is still empty.";
        }

        protected override string GetStartsWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' url doesn't start with '{expectedValue}'.";
        }

        protected override string GetDoesNotStartWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' url starts with '{expectedValue}'.";
        }

        protected override string GetEndsWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' url doesn't end with '{expectedValue}'.";
        }

        protected override string GetDoesNotEndWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' url ends with '{expectedValue}'.";
        }

        protected override string GetContainsError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' url doesn't contain '{expectedValue}' yet.";
        }

        protected override string GetDoesNotContainError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' url contains '{expectedValue}'.";
        }

        protected override string GetMatchesError(string latestValue, Regex regex)
        {
            return $"'{latestValue}' url doesn't match '{regex}'.";
        }

        protected override string GetDoesNotMatchError(string latestValue, Regex regex)
        {
            return $"'{latestValue}' url matches '{regex}'.";
        }
    }
}
