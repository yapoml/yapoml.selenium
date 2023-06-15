using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class TextConditions<TConditions> : TextualConditions<TConditions>
    {
        private readonly IElementHandler _elementHandler;

        public TextConditions(TConditions conditions, IElementHandler elementHandler, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {
            _elementHandler = elementHandler;
        }

        protected override Func<string> FetchValueFunc => () => RelocateOnStaleReference(() => _elementHandler.Locate().Text);

        protected override string GetIsError(string latestValue, string expectedValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{expectedValue}' yet.{GetDifference(expectedValue, latestValue)}";
        }

        protected override string GetIsNotError(string latestValue, string expectedValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still '{expectedValue}'.{GetDifference(expectedValue, latestValue)}";
        }

        protected override string GetIsEmptyError(string latestValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not empty yet.";
        }

        protected override string GetIsNotEmptyError(string latestValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still empty.";
        }

        protected override string GetStartsWithError(string latestValue, string expectedValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{latestValue}' yet.";
        }

        protected override string GetDoesNotStartWithError(string latestValue, string expectedValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component starts with '{expectedValue}'.";
        }

        protected override string GetEndsWithError(string latestValue, string expectedValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{expectedValue}' yet.";
        }

        protected override string GetDoesNotEndWithError(string latestValue, string expectedValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component ends with '{expectedValue}'.";
        }

        protected override string GetContainsError(string latestValue, string expectedValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't contain '{expectedValue}' yet.";
        }

        protected override string GetDoesNotContainError(string latestValue, string expectedValue)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component contains '{expectedValue}'.";
        }

        protected override string GetMatchesError(string latestValue, Regex regex)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't match '{regex}'.";
        }

        protected override string GetDoesNotMatchError(string latestValue, Regex regex)
        {
            return $"Text '{latestValue}' of the {_elementHandler.ComponentMetadata.Name} component matches '{regex}'.";
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
