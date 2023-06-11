using System;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public abstract class NumericConditions<TConditions, TNumber> : Conditions<TConditions>, INumericConditions<TConditions, TNumber> where TNumber : struct, IComparable<TNumber>
    {
        public NumericConditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {

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
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsError(latestValue, value), ex);
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
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsNotError(latestValue, value), ex);
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
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsGreaterThanError(latestValue, value), ex);
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
                Services.Waiter.Until(condition, timeout.Value, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsLessThanError(latestValue, value), ex);
            }

            return _conditions;
        }

        protected abstract string GetIsError(TNumber? latestValue, TNumber expectedValue);

        protected abstract string GetIsNotError(TNumber? latestValue, TNumber expectedValue);

        protected abstract string GetIsGreaterThanError(TNumber? latestValue, TNumber expectedValue);

        protected abstract string GetIsLessThanError(TNumber? latestValue, TNumber expectedValue);
    }
}
