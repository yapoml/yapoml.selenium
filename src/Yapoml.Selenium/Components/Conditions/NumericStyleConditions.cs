using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class NumericStyleConditions<TConditions, TNumber> : NumericConditions<TConditions, TNumber>
        where TNumber : struct
    {
        private readonly IElementHandler _elementHandler;
        private readonly string _styleName;

        public NumericStyleConditions(TConditions conditions, IElementHandler elementHandler, string styleName, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {
            _elementHandler = elementHandler;
            _styleName = styleName;

            _fetchFunc = () =>
            {
                var value = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }
                else
                {
                    return (TNumber)Convert.ChangeType(value, typeof(TNumber));
                }
            };
        }

        protected override Exception GetIsError(TNumber? latestValue, TNumber expectedValue, Exception innerException)
        {
            return new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{expectedValue}' yet.", innerException);
        }

        protected override Exception GetIsNotError(TNumber? latestValue, TNumber expectedValue, Exception innerException)
        {
            return new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still '{expectedValue}'.", innerException);
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
