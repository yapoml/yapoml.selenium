using OpenQA.Selenium;
using System;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class NumericStyleConditions<TConditions, TNumber> : NumericConditions<TConditions, TNumber>
        where TNumber : struct, IComparable<TNumber>
    {
        private readonly IElementHandler _elementHandler;
        private readonly string _styleName;

        public NumericStyleConditions(TConditions conditions, IElementHandler elementHandler, string styleName, TimeSpan timeout, TimeSpan pollingInterval, string subject, ILogger logger)
            : base(conditions, timeout, pollingInterval, subject, logger)
        {
            _elementHandler = elementHandler;
            _styleName = styleName;
        }

        protected override Func<TNumber?> FetchValueFunc => () =>
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

        protected override string GetIsError(TNumber? latestValue, TNumber expectedValue)
        {
            return $"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{expectedValue}' yet.";
        }

        protected override string GetIsNotError(TNumber? latestValue, TNumber expectedValue)
        {
            return $"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still '{expectedValue}'.";
        }

        protected override string GetIsGreaterThanError(TNumber? latestValue, TNumber expectedValue)
        {
            return $"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still not greater than '{expectedValue}'.";
        }

        protected override string AtLeast(TNumber? latestValue, TNumber expectedValue)
        {
            return $"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still not equal to or greater than '{expectedValue}'.";
        }

        protected override string GetIsLessThanError(TNumber? latestValue, TNumber expectedValue)
        {
            return $"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still not less than '{expectedValue}'.";
        }

        protected override string GetAtMostError(TNumber? latestValue, TNumber expectedValue)
        {
            return $"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still not equal to or less than '{expectedValue}'.";
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
