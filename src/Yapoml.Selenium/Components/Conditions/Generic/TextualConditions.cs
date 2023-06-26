using System;
using System.Text.RegularExpressions;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public abstract class TextualConditions<TConditions> : Conditions<TConditions>, ITextualConditions<TConditions>
    {
        public TextualConditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {

        }

        protected abstract Func<string> FetchValueFunc { get; }

        public TConditions Is(string value, TimeSpan? timeout = default)
        {
            return Is(value, StringComparison.CurrentCulture, timeout);
        }

        public TConditions Is(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.Equals(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions IsNot(string value, TimeSpan? timeout = default)
        {
            return IsNot(value, StringComparison.CurrentCulture, timeout);
        }

        public TConditions IsNot(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.Equals(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsNotError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions IsEmpty(TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.Equals(string.Empty);
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsEmptyError(latestValue), ex);
            }

            return _conditions;
        }

        public TConditions IsNotEmpty(TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return !latestValue.Equals(string.Empty);
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsNotEmptyError(latestValue), ex);
            }

            return _conditions;
        }

        public TConditions StartsWith(string value, TimeSpan? timeout = default)
        {
            return StartsWith(value, StringComparison.CurrentCulture, timeout);
        }

        public TConditions StartsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.StartsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetStartsWithError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions DoesNotStartWith(string value, TimeSpan? timeout = default)
        {
            return DoesNotStartWith(value, StringComparison.CurrentCulture, timeout);
        }

        public TConditions DoesNotStartWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.StartsWith(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetDoesNotStartWithError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions EndsWith(string value, TimeSpan? timeout = default)
        {
            return EndsWith(value, StringComparison.CurrentCulture, timeout);
        }

        public TConditions EndsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.EndsWith(value, comparisonType);
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetEndsWithError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions DoesNotEndWith(string value, TimeSpan? timeout = default)
        {
            return DoesNotEndWith(value, StringComparison.CurrentCulture, timeout);
        }

        public TConditions DoesNotEndWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.EndsWith(value, comparisonType) == false;
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetDoesNotEndWithError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions Contains(string value, TimeSpan? timeout = default)
        {
            return Contains(value, StringComparison.CurrentCulture, timeout);
        }

        public TConditions Contains(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.IndexOf(value, comparisonType) >= 0;
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetContainsError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions DoesNotContain(string value, TimeSpan? timeout = default)
        {
            return DoesNotContain(value, StringComparison.CurrentCulture, timeout);
        }

        public TConditions DoesNotContain(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return latestValue.IndexOf(value, comparisonType) == -1;
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetDoesNotContainError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions Matches(Regex regex, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetMatchesError(latestValue, regex), ex);
            }

            return _conditions;
        }

        public TConditions DoesNotMatch(Regex regex, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return !regex.IsMatch(latestValue);
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetDoesNotMatchError(latestValue, regex), ex);
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
