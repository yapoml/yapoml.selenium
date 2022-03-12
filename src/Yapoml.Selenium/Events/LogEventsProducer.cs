using Yapoml.Logging;
using Yapoml.Options;
using Yapoml.Selenium.Events.Args.WebElement;

namespace Yapoml.Selenium.Events
{
    public class LogEventsProducer
    {
        private ILogger _logger;

        private readonly IEventSource _source;

        public LogEventsProducer(ISpaceOptions spaceOptions)
        {
            _logger = spaceOptions.Get<ILogger>();
            _source = spaceOptions.Get<IEventSource>();

            spaceOptions.OnTypeRegistered += SpaceOptions_OnTypeRegistered;
        }

        private void SpaceOptions_OnTypeRegistered(object sender, TypeRegisteredEventArgs e)
        {
            if (e.Type == typeof(ILogger))
            {
                _logger = e.Instance as ILogger;
            }
        }

        public void Init()
        {
            _source.ComponentEventSource.OnFindingComponent += ComponentEventSource_OnFindingComponent;
            _source.ComponentEventSource.OnFindingComponents += ComponentEventSource_OnFindingComponents;
            _source.ComponentEventSource.OnFoundComponents += ComponentEventSource_OnFoundComponents;
        }

        private void ComponentEventSource_OnFoundComponents(object sender, FoundElementsEventArgs e)
        {
            _logger.Trace($"Found {e.Elements.Count} components");
        }

        private void ComponentEventSource_OnFindingComponents(object sender, FindingElementEventArgs e)
        {
            _logger.Trace($"Finding {e.ComponentName} {e.By}");
        }

        private void ComponentEventSource_OnFindingComponent(object sender, FindingElementEventArgs e)
        {
            _logger.Trace($"Finding {e.ComponentName} {e.By}");
        }
    }
}
