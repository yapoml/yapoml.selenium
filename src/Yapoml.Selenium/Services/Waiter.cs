using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Yapoml.Selenium.Services
{
    public static class Waiter
    {
        public static void Until(Func<bool> condition, TimeSpan timeout, TimeSpan pollingInterval)
        {
            var stopwatch = Stopwatch.StartNew();

            Lazy<List<Exception>> occuredExceptions = new Lazy<List<Exception>>(() => new List<Exception>());

            do
            {
                try
                {
                    var isSuccessful = condition();

                    if (isSuccessful)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    occuredExceptions.Value.Add(ex);
                }

                Thread.Sleep(pollingInterval);
            }
            while (stopwatch.Elapsed <= timeout);

            var timeoutMessageBuilder = new StringBuilder($"Condition was not satisfied within {timeout.TotalSeconds} seconds when polled every {pollingInterval.TotalSeconds} seconds.");

            if (occuredExceptions.IsValueCreated)
            {
                timeoutMessageBuilder.AppendLine();
                timeoutMessageBuilder.AppendLine("Occured errors:");

                foreach (var occuredExceptionsGroup in occuredExceptions.Value.GroupBy(e => e.Message))
                {
                    timeoutMessageBuilder.AppendLine($" - {occuredExceptionsGroup.Key} ({occuredExceptionsGroup.Count()} times)");
                }
            }

            throw new TimeoutException(timeoutMessageBuilder.ToString());
        }
    }
}
