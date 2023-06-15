using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using Yapoml.Selenium.Components.Conditions.Generic;

namespace Yapoml.Selenium.Components.Conditions
{
    /// <summary>
    /// Various conditions for page title.
    /// </summary>
    /// <typeparam name="TConditions">Fluent original instance for chaining conditions.</typeparam>
    public class TitleConditions<TConditions> : TextualConditions<TConditions>
    {
        private readonly IWebDriver _webDriver;

        public TitleConditions(IWebDriver webDriver, TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {
            _webDriver = webDriver;
        }

        protected override Func<string> FetchValueFunc => () => _webDriver.Title;

        protected override string GetIsError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' title doesn't equal to '{expectedValue}'.{GetDifference(expectedValue, latestValue)}";
        }

        protected override string GetIsNotError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' title doesn't equal to '{expectedValue}'.{GetDifference(expectedValue, latestValue)}";
        }

        protected override string GetIsEmptyError(string latestValue)
        {
            return $"'{latestValue}' title is not empty yet.";
        }

        protected override string GetIsNotEmptyError(string latestValue)
        {
            return $"'{latestValue}' title is still empty.";
        }

        protected override string GetStartsWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' title doesn't start with '{expectedValue}'.";
        }

        protected override string GetDoesNotStartWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' title starts with '{expectedValue}'.";
        }

        protected override string GetEndsWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' title doesn't end with '{expectedValue}'.";
        }

        protected override string GetDoesNotEndWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' title ends with '{expectedValue}'.";
        }

        protected override string GetContainsError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' title doesn't contain '{expectedValue}' yet.";
        }

        protected override string GetDoesNotContainError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' title contains '{expectedValue}'.";
        }

        protected override string GetMatchesError(string latestValue, Regex regex)
        {
            return $"'{latestValue}' title doesn't match '{regex}'.";
        }

        protected override string GetDoesNotMatchError(string latestValue, Regex regex)
        {
            return $"'{latestValue}' title matches '{regex}'.";
        }
    }
}
