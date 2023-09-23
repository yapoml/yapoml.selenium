using System;
using Yapoml.Framework.Logging;

namespace Yapoml.Selenium.Components.Conditions.Generic
{
    public abstract class Conditions<TConditions>
    {
        protected readonly TConditions _conditions;
        protected readonly TimeSpan _timeout;
        protected readonly TimeSpan _pollingInterval;
        protected readonly ILogger _logger;

        public Conditions(TConditions conditions, TimeSpan timeout, TimeSpan pollingInterval, ILogger logger)
        {
            _conditions = conditions;
            _timeout = timeout;
            _pollingInterval = pollingInterval;
            _logger = logger;
        }

        protected string GetDifference(string phrase, string first, string second)
        {
            if (first is not null && second is not null)
            {
                return Environment.NewLine + Formatters.StringFormatter.Format($"  {phrase} ", new string(' ', phrase.Length + 3), first, second) + Environment.NewLine;
            }
            else
            {
                return null;
            }
        }
    }
}
