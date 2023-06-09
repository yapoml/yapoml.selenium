using System;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public abstract class NumericConditions<TConditions, TNumber> : Conditions<TConditions> where TNumber : struct
    {
        public NumericConditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {

        }

        protected abstract Func<TNumber?> FetchValueFunc { get; }

        public TConditions Is(TNumber value)
        {
            return Is(value, _timeout);
        }

        public TConditions Is(TNumber value, TimeSpan timeout)
        {
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
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsError(latestValue, value), ex);
            }

            return _conditions;
        }

        public TConditions IsNot(TNumber value)
        {
            return IsNot(value, _timeout);
        }

        public TConditions IsNot(TNumber value, TimeSpan timeout)
        {
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
                Services.Waiter.Until(condition, timeout, _pollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException(GetIsNotError(latestValue, value), ex);
            }

            return _conditions;
        }

        protected abstract string GetIsError(TNumber? latestValue, TNumber expectedValue);

        protected abstract string GetIsNotError(TNumber? latestValue, TNumber expectedValue);
    }
}
