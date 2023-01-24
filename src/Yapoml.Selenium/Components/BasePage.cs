using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public abstract class BasePage
    {
        public BasePage(IWebDriver webDriver, IElementHandlerRepository elementHandlerRepository, ISpaceOptions spaceOptions)
        {
            WebDriver = webDriver;
            ElementHandlerRepository = elementHandlerRepository;
            SpaceOptions = spaceOptions;

            EventSource = spaceOptions.Services.Get<IEventSource>();
        }

        protected IWebDriver WebDriver { get; }

        protected IElementHandlerRepository ElementHandlerRepository { get; }

        protected ISpaceOptions SpaceOptions { get; }

        protected IEventSource EventSource;
    }
}
