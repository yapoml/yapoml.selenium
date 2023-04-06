using OpenQA.Selenium;
using System;

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

            bool? condition()
            {
                latestTitle = _webDriver.Title;

                if (latestTitle.Equals(value, comparisonType))
                {
                    return true;
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
                throw Services.Waiter.BuildTimeoutException($"'{latestTitle}' title doesn't equal to '{value}'.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestTitle = _webDriver.Title;

                if (!latestTitle.Equals(value, comparisonType))
                {
                    return true;
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
                throw Services.Waiter.BuildTimeoutException($"'{latestTitle}' title doesn't equal to '{value}'.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestTitle = _webDriver.Title;

                if (latestTitle.StartsWith(value, comparisonType))
                {
                    return true;
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
                throw Services.Waiter.BuildTimeoutException($"'{latestTitle}' title doesn't start with '{value}'.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestTitle = _webDriver.Title;

                if (!latestTitle.StartsWith(value, comparisonType))
                {
                    return true;
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
                throw Services.Waiter.BuildTimeoutException($"'{latestTitle}' title starts with '{value}'.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestTitle = _webDriver.Title;

                if (latestTitle.EndsWith(value, comparisonType))
                {
                    return true;
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
                throw Services.Waiter.BuildTimeoutException($"'{latestTitle}' title doesn't end with '{value}'.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestTitle = _webDriver.Title;

                if (!latestTitle.EndsWith(value, comparisonType))
                {
                    return true;
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
                throw Services.Waiter.BuildTimeoutException($"'{latestTitle}' title ends with '{value}'.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestValue = _webDriver.Title;

                if (latestValue.IndexOf(value, comparisonType) >= 0)
                {
                    return true;
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
                throw Services.Waiter.BuildTimeoutException($"'{latestValue}' title doesn't contain '{value}' yet.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestValue = _webDriver.Title;

                if (latestValue.IndexOf(value, comparisonType) == -1)
                {
                    return true;
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
                throw Services.Waiter.BuildTimeoutException($"'{latestValue}' title contains '{value}'.", null, actualTimeout, actualPollingInterval, null);
            }

            return _conditions;
        }
    }
}
