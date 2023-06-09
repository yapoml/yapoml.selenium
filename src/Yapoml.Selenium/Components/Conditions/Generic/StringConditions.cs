using System;
using System.Text.RegularExpressions;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public abstract class StringConditions<TConditions> : Conditions<TConditions>
    {
        protected Func<string> _fetchFunc;

        public StringConditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {

        }

        public TConditions Is(string value)
        {
            return Is(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public TConditions Is(string value, TimeSpan timeout)
        {
            return Is(value, StringComparison.CurrentCultureIgnoreCase, timeout);
        }

        public TConditions Is(string value, StringComparison comparisonType)
        {
            return Is(value, comparisonType, _timeout);
        }

        public TConditions Is(string value, StringComparison comparisonType, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return latestValue.Equals(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetIsError(latestValue, value, ex);
            }

            return _conditions;
        }

        public TConditions IsNot(string value)
        {
            return IsNot(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public TConditions IsNot(string value, TimeSpan timeout)
        {
            return IsNot(value, StringComparison.CurrentCultureIgnoreCase, timeout);
        }

        public TConditions IsNot(string value, StringComparison comparisonType)
        {
            return IsNot(value, comparisonType, _timeout);
        }

        public TConditions IsNot(string value, StringComparison comparisonType, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return latestValue.Equals(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetIsNotError(latestValue, value, ex);
            }

            return _conditions;
        }

        public TConditions StartsWith(string value)
        {
            return StartsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public TConditions StartsWith(string value, TimeSpan timeout)
        {
            return StartsWith(value, StringComparison.CurrentCultureIgnoreCase, timeout);
        }

        public TConditions StartsWith(string value, StringComparison comparisonType)
        {
            return StartsWith(value, comparisonType, _timeout);
        }

        public TConditions StartsWith(string value, StringComparison comparisonType, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return latestValue.StartsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetStartsWithError(latestValue, value, ex);
            }

            return _conditions;
        }

        public TConditions DoesNotStartWith(string value)
        {
            return DoesNotStartWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public TConditions DoesNotStartWith(string value, TimeSpan timeout)
        {
            return DoesNotStartWith(value, StringComparison.CurrentCultureIgnoreCase, timeout);
        }

        public TConditions DoesNotStartWith(string value, StringComparison comparisonType)
        {
            return DoesNotStartWith(value, comparisonType, _timeout);
        }

        public TConditions DoesNotStartWith(string value, StringComparison comparisonType, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return latestValue.StartsWith(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetDoesNotStartWithError(latestValue, value, ex);
            }

            return _conditions;
        }

        public TConditions EndsWith(string value)
        {
            return EndsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public TConditions EndsWith(string value, TimeSpan timeout)
        {
            return EndsWith(value, StringComparison.CurrentCultureIgnoreCase, timeout);
        }

        public TConditions EndsWith(string value, StringComparison comparisonType)
        {
            return EndsWith(value, comparisonType, _timeout);
        }

        public TConditions EndsWith(string value, StringComparison comparisonType, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return latestValue.EndsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetEndsWithError(latestValue, value, ex);
            }

            return _conditions;
        }

        public TConditions DoesNotEndWith(string value)
        {
            return DoesNotEndWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public TConditions DoesNotEndWith(string value, TimeSpan timeout)
        {
            return DoesNotEndWith(value, StringComparison.CurrentCultureIgnoreCase, timeout);
        }

        public TConditions DoesNotEndWith(string value, StringComparison comparisonType)
        {
            return DoesNotEndWith(value, comparisonType, _timeout);
        }

        public TConditions DoesNotEndWith(string value, StringComparison comparisonType, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return latestValue.EndsWith(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetDoesNotEndWithError(latestValue, value, ex);
            }

            return _conditions;
        }

        public TConditions Contains(string value)
        {
            return Contains(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public TConditions Contains(string value, TimeSpan timeout)
        {
            return Contains(value, StringComparison.CurrentCultureIgnoreCase, timeout);
        }

        public TConditions Contains(string value, StringComparison comparisonType)
        {
            return Contains(value, comparisonType, _timeout);
        }

        public TConditions Contains(string value, StringComparison comparisonType, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return latestValue.IndexOf(value, comparisonType) >= 0;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetContainsError(latestValue, value, ex);
            }

            return _conditions;
        }

        public TConditions DoesNotContain(string value)
        {
            return DoesNotContain(value, StringComparison.CurrentCultureIgnoreCase);
        }

        public TConditions DoesNotContain(string value, TimeSpan timeout)
        {
            return DoesNotContain(value, StringComparison.CurrentCultureIgnoreCase, timeout);
        }

        public TConditions DoesNotContain(string value, StringComparison comparisonType)
        {
            return DoesNotContain(value, comparisonType, _timeout);
        }

        public TConditions DoesNotContain(string value, StringComparison comparisonType, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return latestValue.IndexOf(value, comparisonType) == -1;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetDoesNotContainError(latestValue, value, ex);
            }

            return _conditions;
        }

        public TConditions Matches(Regex regex)
        {
            return Matches(regex, _timeout);
        }

        public TConditions Matches(Regex regex, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetMatchesError(latestValue, regex, ex);
            }

            return _conditions;
        }

        public TConditions DoesNotMatch(Regex regex)
        {
            return DoesNotMatch(regex, _timeout);
        }

        public TConditions DoesNotMatch(Regex regex, TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

                return !regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw GetDoesNotMatchError(latestValue, regex, ex);
            }

            return _conditions;
        }

        protected virtual Exception GetIsError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' is not '{expectedValue}' yet.", innerException);
        }

        protected virtual Exception GetIsNotError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' is still '{expectedValue}'.", innerException);
        }

        protected virtual Exception GetStartsWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' is not '{latestValue}' yet.", innerException);
        }

        protected virtual Exception GetDoesNotStartWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' starts with '{expectedValue}'.", innerException);
        }

        protected virtual Exception GetEndsWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' is not '{expectedValue}' yet.", innerException);
        }

        protected virtual Exception GetDoesNotEndWithError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' ends with '{expectedValue}'.", innerException);
        }

        protected virtual Exception GetContainsError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' doesn't contain '{expectedValue}' yet.", innerException);
        }

        protected virtual Exception GetDoesNotContainError(string latestValue, string expectedValue, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' contains '{expectedValue}'.", innerException);
        }

        protected virtual Exception GetMatchesError(string latestValue, Regex regex, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' doesn't match '{regex}'.", innerException);
        }

        protected virtual Exception GetDoesNotMatchError(string latestValue, Regex regex, Exception innerException)
        {
            return new TimeoutException($"Text '{latestValue}' matches '{regex}'.", innerException);
        }
    }
}
