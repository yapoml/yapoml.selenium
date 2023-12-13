using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Components.Conditions
{
    /// <summary>
    /// Various conditions for awaiting url's path.
    /// </summary>
    /// <typeparam name="TConditions">Fluent original instance for chaining conditions.</typeparam>
    public class UrlPathConditions<TConditions> : TextualConditions<TConditions>
    {
        private readonly IWebDriver _webDriver;

        private readonly PageMetadata _pageMetadata;

        public UrlPathConditions(IWebDriver webDriver, TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval, PageMetadata pageMetadata, ILogger logger)
            : base(conditions, timeout, pollingInterval, $"{pageMetadata.Name} page url path", logger)
        {
            _webDriver = webDriver;
            _pageMetadata = pageMetadata;
        }

        protected override Func<string> FetchValueFunc => () => new Uri(_webDriver.Url).AbsolutePath;

        public override NumericConditions<TConditions, int> Length
            => new TextualLengthConditons<TConditions>(_conditions, _timeout, _pollingInterval, FetchValueFunc, $"{_pageMetadata.Name} page url", _logger);

        protected override string GetIsError(string latestValue, string expectedValue)
        {
            return $"{_pageMetadata.Name} page url path is not '{expectedValue}',{GetDifference("it was:", expectedValue, latestValue)}";
        }

        protected override string GetIsNotError(string latestValue, string expectedValue)
        {
            return $"{_pageMetadata.Name} page url path is '{latestValue}', when expected to be not.";
        }

        protected override string GetIsEmptyError(string latestValue)
        {
            return $"{_pageMetadata.Name} page url path '{latestValue}' is not empty, when expected to be empty.";
        }

        protected override string GetIsNotEmptyError(string latestValue)
        {
            return $"{_pageMetadata.Name} page url path is empty, when expected to be not empty.";
        }

        protected override string GetStartsWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' {_pageMetadata.Name} page url path doesn't start with '{expectedValue}'.";
        }

        protected override string GetDoesNotStartWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' {_pageMetadata.Name} page url path starts with '{expectedValue}'.";
        }

        protected override string GetEndsWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' {_pageMetadata.Name} page url path doesn't end with '{expectedValue}'.";
        }

        protected override string GetDoesNotEndWithError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' {_pageMetadata.Name} page url path ends with '{expectedValue}'.";
        }

        protected override string GetContainsError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' {_pageMetadata.Name} page url path doesn't contain '{expectedValue}' yet.";
        }

        protected override string GetDoesNotContainError(string latestValue, string expectedValue)
        {
            return $"'{latestValue}' {_pageMetadata.Name} page url path contains '{expectedValue}'.";
        }

        protected override string GetMatchesError(string latestValue, Regex regex)
        {
            return $"'{latestValue}' {_pageMetadata.Name} page url path doesn't match '{regex}'.";
        }

        protected override string GetDoesNotMatchError(string latestValue, Regex regex)
        {
            return $"'{latestValue}' {_pageMetadata.Name} page url path matches '{regex}'.";
        }
    }
}
