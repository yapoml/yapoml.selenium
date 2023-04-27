using System;
using Yapoml.Framework.Logging;
using Yapoml.Framework.Options;

namespace Yapoml.Selenium
{
    public static class LoggingExtensions
    {
        public static ISpaceOptions WithLogger(this ISpaceOptions spaceOptions, ILogger logger)
        {
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            spaceOptions.Services.Register(logger);

            return spaceOptions;
        }
    }

    public class NullLogger : ILogger
    {
        public void Trace(string message)
        {
            // do nothing
        }
    }
}
