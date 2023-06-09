using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class StringAttributeConditions<TConditions> : StringConditions<TConditions>
    {
        private readonly IElementHandler _elementHandler;
        private readonly string _attributeName;

        public StringAttributeConditions(TConditions conditions, IElementHandler elementHandler, string attributeName, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {
            _elementHandler = elementHandler;
            _attributeName = attributeName;

            _fetchFunc = () => RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));
        }

        protected override Exception GetIsError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{expectedValue}' yet.", innerException);
        }

        protected override Exception GetIsNotError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still '{expectedValue}'.", innerException);
        }

        protected override Exception GetStartsWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component does not start with '{expectedValue}' yet.", innerException);
        }

        protected override Exception GetDoesNotStartWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component starts with '{expectedValue}'.", innerException);
        }

        protected override Exception GetEndsWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component does not end with'{expectedValue}' yet.", innerException);
        }

        protected override Exception GetDoesNotEndWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component ends with '{expectedValue}'.", innerException);
        }

        protected override Exception GetContainsError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't contain '{expectedValue}' yet.", innerException);
        }

        protected override Exception GetDoesNotContainError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component contains '{expectedValue}'.", innerException);
        }

        protected override Exception GetMatchesError(string latestValue, Regex regex, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't match '{regex}'.", innerException);
        }

        protected override Exception GetDoesNotMatchError(string latestValue, Regex regex, Exception innerException)
        {
            return new TimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component matches '{regex}'.", innerException);
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
