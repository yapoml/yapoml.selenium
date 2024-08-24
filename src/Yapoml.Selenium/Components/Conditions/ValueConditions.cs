using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class ValueConditions<TConditions> : TextualConditions<TConditions>
    {
        private IElementHandler _elementHandler;

        public ValueConditions(TConditions conditions, IElementHandler elementHandler, TimeSpan timeout, TimeSpan pollingInterval, string subject, ILogger logger)
            : base(conditions, timeout, pollingInterval, subject, logger)
        {
            _elementHandler = elementHandler;
        }

        public override NumericConditions<TConditions, int> Length => new TextualLengthConditons<TConditions>(_conditions, _timeout, _pollingInterval, FetchValueFunc, $"value of {_elementHandler.ComponentMetadata.Name}", _logger);

        protected override Func<string> FetchValueFunc => () => RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute("value"));

        protected override string GetIsError(string latestValue, string expectedValue)
        {
            return $"Value of the {_elementHandler.ComponentMetadata.Name} is not '{expectedValue}',{GetDifference("it was:", expectedValue, latestValue)}";
        }

        protected override string GetIsNotError(string latestValue, string expectedValue)
        {
            return $"Value of the {_elementHandler.ComponentMetadata.Name} is '{latestValue}', when expected to be not.";
        }

        protected override string GetIsEmptyError(string latestValue)
        {
            return $"StValueyle '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} is not empty, when expected to be empty.";
        }

        protected override string GetIsNotEmptyError(string latestValue)
        {
            return $"Value of the {_elementHandler.ComponentMetadata.Name} is empty, when expected to be not empty.";
        }

        protected override string GetStartsWithError(string latestValue, string expectedValue)
        {
            return $"Value '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component does not start with '{expectedValue}' yet.";
        }

        protected override string GetDoesNotStartWithError(string latestValue, string expectedValue)
        {
            return $"Value '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component starts with '{expectedValue}'.";
        }

        protected override string GetEndsWithError(string latestValue, string expectedValue)
        {
            return $"Value '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't end with '{expectedValue}'.";
        }

        protected override string GetDoesNotEndWithError(string latestValue, string expectedValue)
        {
            return $"Value '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component ends with '{expectedValue}'.";
        }

        protected override string GetContainsError(string latestValue, string expectedValue)
        {
            return $"Value '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't contain '{expectedValue}' yet.";
        }

        protected override string GetDoesNotContainError(string latestValue, string expectedValue)
        {
            return $"Value '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component contains '{expectedValue}'.";
        }

        protected override string GetMatchesError(string latestValue, Regex regex)
        {
            return $"Value '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't match '{regex}'.";
        }

        protected override string GetDoesNotMatchError(string latestValue, Regex regex)
        {
            return $"Value '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component matches '{regex}'.";
        }

        private T RelocateOnStaleReference<T>(Func<T> act)
        {
            try
            {
                return act();
            }
            catch (StaleElementReferenceException)
            {
                _elementHandler.Invalidate();

                _elementHandler.Locate();

                return act();
            }
        }
    }
}
