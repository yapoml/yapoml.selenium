using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public abstract class BasePage
    {
        public BasePage(IWebDriver webDriver, IElementHandlerRepository elementHandlerRepository, PageMetadata metadata, ISpaceOptions spaceOptions)
        {
            WebDriver = webDriver;
            ElementHandlerRepository = elementHandlerRepository;
            Metadata = metadata;
            SpaceOptions = spaceOptions;

            EventSource = spaceOptions.Services.Get<IEventSource>();
        }

        protected IWebDriver WebDriver { get; }

        protected IElementHandlerRepository ElementHandlerRepository { get; }

        protected PageMetadata Metadata { get; }

        protected ISpaceOptions SpaceOptions { get; }

        protected IEventSource EventSource;
    }
}
