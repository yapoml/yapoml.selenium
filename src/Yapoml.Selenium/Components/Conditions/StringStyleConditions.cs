using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class StringStyleConditions<TConditions>
    {
        private readonly TConditions _conditions;
        private readonly IElementHandler _elementHandler;
        private readonly string _styleName;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval;

        public StringStyleConditions(TConditions conditions, IElementHandler elementHandler, string styleName, TimeSpan timeout, TimeSpan pollingInterval)
        {
            _conditions = conditions;
            _elementHandler = elementHandler;
            _styleName = styleName;
            _timeout = timeout;
            _pollingInterval = pollingInterval;
        }

        public TConditions Is(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return Is(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions Is(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return latestValue?.Equals(value, comparisonType) == true;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{value}' yet.", ex);
            }

            return _conditions;
        }

        public TConditions IsNot(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return IsNot(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions IsNot(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return latestValue?.Equals(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still '{value}'.", ex);
            }

            return _conditions;
        }

        public TConditions StartsWith(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return StartsWith(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions StartsWith(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return latestValue?.StartsWith(value, comparisonType) == true;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{value}' yet.", ex);
            }

            return _conditions;
        }

        public TConditions DoesNotStartWith(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return DoesNotStartWith(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions DoesNotStartWith(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return latestValue?.StartsWith(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component starts with '{value}'.", ex);
            }

            return _conditions;
        }

        public TConditions EndsWith(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return EndsWith(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions EndsWith(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return latestValue?.EndsWith(value, comparisonType) == true;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't end with '{value}'.", ex);
            }

            return _conditions;
        }

        public TConditions DoesNotEndWith(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return DoesNotEndWith(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions DoesNotEndWith(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return latestValue?.EndsWith(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component ends with '{value}'.", ex);
            }

            return _conditions;
        }

        public TConditions Contains(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return Contains(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions Contains(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return latestValue?.IndexOf(value, comparisonType) >= 0;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't contain '{value}' yet.", ex);
            }

            return _conditions;
        }

        public TConditions DoesnNotContain(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return DoesnNotContain(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions DoesnNotContain(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return latestValue?.IndexOf(value, comparisonType) == -1;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component contains '{value}'.", ex);
            }

            return _conditions;
        }

        public TConditions Matches(Regex regex, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't match '{regex}'.", ex);
            }

            return _conditions;
        }

        public TConditions DoesNotMatch(Regex regex, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(_styleName));

                return !regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Style '{_styleName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component matches '{regex}'.", ex);
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
