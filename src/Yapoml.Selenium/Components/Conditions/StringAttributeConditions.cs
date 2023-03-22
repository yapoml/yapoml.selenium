using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class StringAttributeConditions<TConditions>
    {
        private readonly TConditions _conditions;
        private readonly IElementHandler _elementHandler;
        private readonly string _attributeName;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval;

        public StringAttributeConditions(TConditions conditions, IElementHandler elementHandler, string attributeName, TimeSpan timeout, TimeSpan pollingInterval)
        {
            _conditions = conditions;
            _elementHandler = elementHandler;
            _attributeName = attributeName;
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

            bool? condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

                if (latestValue?.Equals(value, comparisonType) == true)
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
                throw Services.Waiter.BuildTimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{value}' yet.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

                if (latestValue?.Equals(value, comparisonType) == false)
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
                throw Services.Waiter.BuildTimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is still '{value}'.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

                if (latestValue?.StartsWith(value, comparisonType) == true)
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
                throw Services.Waiter.BuildTimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{value}' yet.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

                if (latestValue?.StartsWith(value, comparisonType) == false)
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
                throw Services.Waiter.BuildTimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component starts with '{value}'.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

                if (latestValue?.EndsWith(value, comparisonType) == true)
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
                throw Services.Waiter.BuildTimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component is not '{value}' yet.", null, actualTimeout, actualPollingInterval, null);
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

            bool? condition()
            {
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

                if (latestValue?.EndsWith(value, comparisonType) == false)
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
                throw Services.Waiter.BuildTimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component ends with '{value}'.", null, actualTimeout, actualPollingInterval, null);
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
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

                if (latestValue?.IndexOf(value, comparisonType) >= 0)
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
                throw Services.Waiter.BuildTimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component doesn't contain '{value}' yet.", null, actualTimeout, actualPollingInterval, null);
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
                latestValue = RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(_attributeName));

                if (latestValue?.IndexOf(value, comparisonType) == -1)
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
                throw Services.Waiter.BuildTimeoutException($"Attribute '{_attributeName} = {latestValue}' of the {_elementHandler.ComponentMetadata.Name} component contains '{value}'.", null, actualTimeout, actualPollingInterval, null);
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
