using Yapoml.Framework.Logging;
using Yapoml.Framework.Options;

namespace Yapoml.Selenium
{
    public static class LoggingExtensions
    {
        public static ISpaceOptions WithLogger(this ISpaceOptions spaceOptions, ILogger logger = null)
        {
            if (logger is null)
            {
                logger = new ConsoleLogger();
            }

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
