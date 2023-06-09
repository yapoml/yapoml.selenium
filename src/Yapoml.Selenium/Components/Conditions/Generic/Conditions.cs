using System;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public abstract class Conditions<TConditions>
    {
        protected readonly TConditions _conditions;
        protected readonly TimeSpan _timeout;
        protected readonly TimeSpan _pollingInterval;

        public Conditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval)
        {
            _conditions = conditions;
            _timeout = timeout;
            _pollingInterval = pollingInterval;
        }
    }
}
