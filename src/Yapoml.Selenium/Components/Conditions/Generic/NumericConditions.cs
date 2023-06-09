using System;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public abstract class NumericConditions<TConditions, TNumber> : Conditions<TConditions> where TNumber : struct
    {
        protected Func<TNumber?> _fetchFunc;

        public NumericConditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
            : base(conditions, timeout, pollingInterval)
        {

        }

        public TConditions Is(TNumber value)
        {
            return Is(value, _timeout);
        }

        public TConditions Is(TNumber value, TimeSpan timeout)
        {
            TNumber? latestValue = null;

            bool condition()
            {
                latestValue = _fetchFunc();

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
                throw GetIsError(latestValue, value, ex);
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
                latestValue = _fetchFunc();

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
                throw GetIsNotError(latestValue, value, ex);
            }

            return _conditions;
        }

        protected virtual Exception GetIsError(Nullable<TNumber> latestValue, TNumber expectedValue, Exception innerException)
        {
            return new TimeoutException($"The '{latestValue}' is not '{expectedValue}' yet.", innerException);
        }

        protected virtual Exception GetIsNotError(Nullable<TNumber> latestValue, TNumber expectedValue, Exception innerException)
        {
            return new TimeoutException($"The '{latestValue}' is still '{expectedValue}'.", innerException);
        }
    }
}
