using System;
using System.Text.RegularExpressions;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public abstract class StringConditions<TConditions> : Conditions<TConditions>, IStringConditions<TConditions>
    {
        public StringConditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {

        }

        protected abstract Func<string> FetchValueFunc { get; }

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
                latestValue = FetchValueFunc();

                return latestValue.Equals(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsError(latestValue, value), ex);
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
                latestValue = FetchValueFunc();

                return latestValue.Equals(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsNotError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions IsEmpty()
        {
            return IsEmpty(_timeout);
        }

        public TConditions IsEmpty(TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.Equals(string.Empty);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsEmptyError(latestValue), ex);
            }

            return _conditions;
        }

        public TConditions IsNotEmpty()
        {
            return IsNotEmpty(_timeout);
        }

        public TConditions IsNotEmpty(TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return !latestValue.Equals(string.Empty);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsNotEmptyError(latestValue), ex);
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
                latestValue = FetchValueFunc();

                return latestValue.StartsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetStartsWithError(latestValue, value), ex);
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
                latestValue = FetchValueFunc();

                return latestValue.StartsWith(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetDoesNotStartWithError(latestValue, value), ex);
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
                latestValue = FetchValueFunc();

                return latestValue.EndsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetEndsWithError(latestValue, value), ex);
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
                latestValue = FetchValueFunc();

                return latestValue.EndsWith(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetDoesNotEndWithError(latestValue, value), ex);
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
                latestValue = FetchValueFunc();

                return latestValue.IndexOf(value, comparisonType) >= 0;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetContainsError(latestValue, value), ex);
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
                latestValue = FetchValueFunc();

                return latestValue.IndexOf(value, comparisonType) == -1;
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetDoesNotContainError(latestValue, value), ex);
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
                latestValue = FetchValueFunc();

                return regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetMatchesError(latestValue, regex), ex);
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
                latestValue = FetchValueFunc();

                return !regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetDoesNotMatchError(latestValue, regex), ex);
            }

            return _conditions;
        }

        protected abstract string GetIsError(string latestValue, string expectedValue);

        protected abstract string GetIsNotError(string latestValue, string expectedValue);

        protected abstract string GetIsEmptyError(string latestValue);

        protected abstract string GetIsNotEmptyError(string latestValue);

        protected abstract string GetStartsWithError(string latestValue, string expectedValue);

        protected abstract string GetDoesNotStartWithError(string latestValue, string expectedValue);

        protected abstract string GetEndsWithError(string latestValue, string expectedValue);

        protected abstract string GetDoesNotEndWithError(string latestValue, string expectedValue);

        protected abstract string GetContainsError(string latestValue, string expectedValue);

        protected abstract string GetDoesNotContainError(string latestValue, string expectedValue);

        protected abstract string GetMatchesError(string latestValue, Regex regex);

        protected abstract string GetDoesNotMatchError(string latestValue, Regex regex);
    }
}
