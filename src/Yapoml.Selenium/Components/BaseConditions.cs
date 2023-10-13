using System;

namespace Yapoml.Selenium.Components
{
    public abstract class BaseConditions<TSelf>
    {
        protected TSelf _self;

        protected BaseConditions(TimeSpan timeout, TimeSpan pollingInterval)
        {
            Timeout = timeout;
            PollingInterval = pollingInterval;
        }

        protected TimeSpan Timeout { get; }
        protected TimeSpan PollingInterval { get; }
    }
}
