using System;
using System.Text.RegularExpressions;
using Yapoml.Framework.Logging;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    /// <inheritdoc cref="ITextualConditions{TConditions}"/>
    public abstract class TextualConditions<TSelf> : Conditions<TSelf>, ITextualConditions<TSelf>
    {
        protected readonly string _subject;

        protected TextualConditions(TSelf conditions, TimeSpan timeout, TimeSpan pollingInterval, string subject, ILogger logger)
            : base(conditions, timeout, pollingInterval, logger)
        {
            _subject = subject;
        }

        protected abstract Func<string> FetchValueFunc { get; }

        public abstract NumericConditions<TSelf, int> Length { get; }

        public TSelf Is(string value, TimeSpan? timeout = default)
        {
            return Is(value, StringComparison.CurrentCulture, timeout);
        }

        public TSelf Is(string value, StringComparison comparisonType, TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is {value}"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TSelf IsNot(string value, TimeSpan? timeout = default)
        {
            return IsNot(value, StringComparison.CurrentCulture, timeout);
        }

        public TSelf IsNot(string value, StringComparison comparisonType, TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is not {value}"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsNotError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TSelf IsEmpty(TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is empty"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsEmptyError(latestValue), ex);
            }

            return _conditions;
        }

        public TSelf IsNotEmpty(TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return !string.IsNullOrEmpty(latestValue);
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is not empty"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsNotEmptyError(latestValue), ex);
            }

            return _conditions;
        }

        public TSelf StartsWith(string value, TimeSpan? timeout = default)
        {
            return StartsWith(value, StringComparison.CurrentCulture, timeout);
        }

        public TSelf StartsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} starts with {value}"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetStartsWithError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TSelf DoesNotStartWith(string value, TimeSpan? timeout = default)
        {
            return DoesNotStartWith(value, StringComparison.CurrentCulture, timeout);
        }

        public TSelf DoesNotStartWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return !latestValue.StartsWith(value, comparisonType);
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} does not start with {value}"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetDoesNotStartWithError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TSelf EndsWith(string value, TimeSpan? timeout = default)
        {
            return EndsWith(value, StringComparison.CurrentCulture, timeout);
        }

        public TSelf EndsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} ends with {value}"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetEndsWithError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TSelf DoesNotEndWith(string value, TimeSpan? timeout = default)
        {
            return DoesNotEndWith(value, StringComparison.CurrentCulture, timeout);
        }

        public TSelf DoesNotEndWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                return !latestValue.EndsWith(value, comparisonType);
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} does not end with {value}"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetDoesNotEndWithError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TSelf Contains(string value, TimeSpan? timeout = default)
        {
            return Contains(value, StringComparison.CurrentCulture, timeout);
        }

        public TSelf Contains(string value, StringComparison comparisonType, TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} contains {value}"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetContainsError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TSelf DoesNotContain(string value, TimeSpan? timeout = default)
        {
            return DoesNotContain(value, StringComparison.CurrentCulture, timeout);
        }

        public TSelf DoesNotContain(string value, StringComparison comparisonType, TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} does not contain {value}"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetDoesNotContainError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TSelf Matches(Regex regex, TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} matches {regex} regular expression"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetMatchesError(latestValue, regex), ex);
            }

            return _conditions;
        }

        public TSelf DoesNotMatch(Regex regex, TimeSpan? timeout = default)
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
                using (var scope = _logger.BeginLogScope($"Expect {_subject} does not match {regex} regular expression"))
                {
                    scope.Execute(() =>
                    {
                        Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
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
