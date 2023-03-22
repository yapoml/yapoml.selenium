using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class NumericStyleConditions<TConditions, TNumber> where TNumber : struct
    {
        private readonly TConditions _conditions;
        private readonly IElementHandler _elementHandler;
        private readonly string _styleName;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval;

        public NumericStyleConditions(TConditions conditions, IElementHandler elementHandler, string styleName, TimeSpan timeout, TimeSpan pollingInterval)
        {
            _conditions = conditions;
            _elementHandler = elementHandler;
            _styleName = styleName;
            _timeout = timeout;
            _pollingInterval = pollingInterval;
        }

        public TConditions Is(TNumber value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool? condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                if (latestValue != null)
                {
                    var latestNumericValue = (TNumber)Convert.ChangeType(latestValue, typeof(TNumber));

                    if (latestNumericValue.Equals(value))
                    {
                        return true;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException)
            {
                throw Services.Waiter.BuildTimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{value}' yet.", null, actualTimeout, actualPollingInterval, null);
            }

            return _conditions;
        }

        public TConditions IsNot(TNumber value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool? condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                if (latestValue != null)
                {
                    var latestNumericValue = (TNumber)Convert.ChangeType(latestValue, typeof(TNumber));

                    if (!latestNumericValue.Equals(value))
                    {
                        return true;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return true;
                }
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException)
            {
                throw Services.Waiter.BuildTimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still '{value}'.", null, actualTimeout, actualPollingInterval, null);
            }

            return _conditions;
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
