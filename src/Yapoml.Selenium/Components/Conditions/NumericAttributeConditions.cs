using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class NumericAttributeConditions<TConditions, TNumber> : NumericConditions<TConditions, TNumber>
        where TNumber : struct
    {
        private readonly IElementHandler _elementHandler;
        private readonly string _attributeName;

        public NumericAttributeConditions(TConditions conditions, IElementHandler elementHandler, string attributeName, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {
            _elementHandler = elementHandler;
            _attributeName = attributeName;
        }

        protected override Func<TNumber?> FetchValueFunc => () =>
        {
            var value = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

            if (value is null)
            {
                return null;
            }
            else
            {
                return (TNumber)Convert.ChangeType(value, typeof(TNumber));
            }
        };

        protected override string GetIsError(TNumber? latestValue, TNumber expectedValue)
        {
            return $"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{expectedValue}' yet.";
        }

        protected override string GetIsNotError(TNumber? latestValue, TNumber expectedValue)
        {
            return $"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still '{expectedValue}'.";
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
