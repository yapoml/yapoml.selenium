using System;
using Yapoml.Framework;
using Yapoml.Framework.Logging;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    /// <inheritdoc cref="INumericConditions{TConditions, TNumber}"/>
    public abstract class NumericConditions<TConditions, TNumber> : Conditions<TConditions>, INumericConditions<TConditions, TNumber> where TNumber : struct, IComparable<TNumber>
    {
        protected readonly string _subject;

        protected NumericConditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval, string subject, ILogger logger)
            : base(conditions, timeout, pollingInterval, logger)
        {
            _subject = subject;
        }

        protected abstract Func<TNumber?> FetchValueFunc { get; }

        public TConditions Is(TNumber value, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            TNumber? latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                if (latestValue != null)
                {
                    return latestValue.Equals(value);
                }
                else
                {
                    return false;
                }
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is {value}"))
                {
                    scope.Execute(() =>
                    {
                        Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions IsNot(TNumber value, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            TNumber? latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                if (latestValue != null)
                {
                    return !latestValue.Equals(value);
                }
                else
                {
                    return true;
                }
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is not {value}"))
                {
                    scope.Execute(() =>
                    {
                        Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsNotError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions IsGreaterThan(TNumber value, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            TNumber? latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                if (latestValue != null)
                {
                    return ((IComparable<TNumber>)latestValue).CompareTo(value) > 0;
                }
                else
                {
                    return false;
                }
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is greater than {value}"))
                {
                    scope.Execute(() =>
                    {
                        Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsGreaterThanError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions AtLeast(TNumber value, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            TNumber? latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                if (latestValue != null)
                {
                    return ((IComparable<TNumber>)latestValue).CompareTo(value) >= 0;
                }
                else
                {
                    return false;
                }
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is equal to or greater than {value}"))
                {
                    scope.Execute(() => 
                    { 
                        Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(AtLeast(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions IsLessThan(TNumber value, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            TNumber? latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                if (latestValue != null)
                {
                    return ((IComparable<TNumber>)latestValue).CompareTo(value) < 0;
                }
                else
                {
                    return false;
                }
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is less than {value}"))
                {
                    scope.Execute(() => 
                    { 
                        Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetIsLessThanError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions AtMost(TNumber value, TimeSpan? timeout = default)
        {
            timeout ??= _timeout;

            TNumber? latestValue = null;

            bool condition()
            {
                latestValue = FetchValueFunc();

                if (latestValue != null)
                {
                    return ((IComparable<TNumber>)latestValue).CompareTo(value) <= 0;
                }
                else
                {
                    return false;
                }
            }

            try
            {
                using (var scope = _logger.BeginLogScope($"Expect {_subject} is less than {value}"))
                {
                    scope.Execute(() =>
                    {
                        Waiter.Until(condition, timeout.Value, _pollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException(GetAtMostError(latestValue, value), ex);
            }

            return _conditions;
        }

        protected abstract string GetIsError(TNumber? latestValue, TNumber expectedValue);

        protected abstract string GetIsNotError(TNumber? latestValue, TNumber expectedValue);

        protected abstract string GetIsGreaterThanError(TNumber? latestValue, TNumber expectedValue);

        protected abstract string AtLeast(TNumber? latestValue, TNumber expectedValue);

        protected abstract string GetIsLessThanError(TNumber? latestValue, TNumber expectedValue);

        protected abstract string GetAtMostError(TNumber? latestValue, TNumber expectedValue);
    }
}
