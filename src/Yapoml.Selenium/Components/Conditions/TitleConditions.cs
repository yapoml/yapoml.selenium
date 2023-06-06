using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;

namespace Yapoml.Selenium.Components.Conditions
{
    /// <summary>
    /// Various conditions for page title.
    /// </summary>
    /// <typeparam name="TConditions">Fluent original instance for chaining conditions.</typeparam>
    public class TitleConditions<TConditions>
    {
        private readonly IWebDriver _webDriver;
        private readonly TConditions _conditions;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval;

        public TitleConditions(IWebDriver webDriver, TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
        {
            _webDriver = webDriver;
            _conditions = conditions;
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

            string latestTitle = null;

            bool condition()
            {
                latestTitle = _webDriver.Title;

                return latestTitle.Equals(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestTitle}' title doesn't equal to '{value}'.", ex);
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

            string latestTitle = null;

            bool condition()
            {
                latestTitle = _webDriver.Title;

                return !latestTitle.Equals(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestTitle}' title doesn't equal to '{value}'.", ex);
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

            string latestTitle = null;

            bool condition()
            {
                latestTitle = _webDriver.Title;

                return latestTitle.StartsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestTitle}' title doesn't start with '{value}'.", ex);
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

            string latestTitle = null;

            bool condition()
            {
                latestTitle = _webDriver.Title;

                return !latestTitle.StartsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestTitle}' title starts with '{value}'.", ex);
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

            string latestTitle = null;

            bool condition()
            {
                latestTitle = _webDriver.Title;

                return latestTitle.EndsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestTitle}' title doesn't end with '{value}'.", ex);
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

            string latestTitle = null;

            bool condition()
            {
                latestTitle = _webDriver.Title;

                return !latestTitle.EndsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestTitle}' title ends with '{value}'.", ex);
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
                latestValue = _webDriver.Title;

                return latestValue.IndexOf(value, comparisonType) >= 0;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestValue}' title doesn't contain '{value}' yet.", ex);
            }

            return _conditions;
        }

        public TConditions DoesNotContain(string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            return DoesNotContain(value, StringComparison.CurrentCultureIgnoreCase, timeout, pollingInterval);
        }

        public TConditions DoesNotContain(string value, StringComparison comparisonType, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            string latestValue = null;

            bool condition()
            {
                latestValue = _webDriver.Title;

                return latestValue.IndexOf(value, comparisonType) == -1;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestValue}' title contains '{value}'.", ex);
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
                latestValue = _webDriver.Title;

                return regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestValue}' title doesn't match '{regex}'.", ex);
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
                latestValue = _webDriver.Title;

                return !regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"'{latestValue}' title matches '{regex}'.", ex);
            }

            return _conditions;
        }
    }
}
