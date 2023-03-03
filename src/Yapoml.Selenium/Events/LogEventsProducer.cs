using Yapoml.Framework.Logging;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Events.Args.Page;
using Yapoml.Selenium.Events.Args.WebElement;

namespace Yapoml.Selenium.Events
{
    public class LogEventsProducer
    {
        private ILogger _logger;

        private readonly IEventSource _source;

        public LogEventsProducer(ISpaceOptions spaceOptions)
        {
            _logger = spaceOptions.Services.Get<ILogger>();
            _source = spaceOptions.Services.Get<IEventSource>();

            spaceOptions.Services.OnTypeRegistered += SpaceOptions_OnTypeRegistered;
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

            _source.PageEventSource.OnPageNavigating += PageEventSource_OnPageNavigating;
        }

        private void PageEventSource_OnPageNavigating(object sender, PageNavigatingEventArgs e)
        {
            _logger.Trace($"Opening {e.Page.GetType().Name} by {e.Uri}");
        }

        private void ComponentEventSource_OnFoundComponents(object sender, FoundElementsEventArgs e)
        {
            _logger.Trace($"Found {e.Elements.Count} {e.ComponentMetadata.Name}");
        }

        private void ComponentEventSource_OnFindingComponents(object sender, FindingElementEventArgs e)
        {
            _logger.Trace($"Finding {e.ComponentMetadata.Name} {e.By}");
        }

        private void ComponentEventSource_OnFindingComponent(object sender, FindingElementEventArgs e)
        {
            _logger.Trace($"Finding {e.ComponentMetadata.Name} {e.By}");
        }
    }
}
