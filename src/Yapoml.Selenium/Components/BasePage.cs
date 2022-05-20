using OpenQA.Selenium;
using Yapoml.Framework.Options;

namespace Yapoml.Selenium.Components
{
    public abstract class BasePage
    {
        public BasePage(IWebDriver webDriver, ISpaceOptions spaceOptions)
        {
            WebDriver = webDriver;
            SpaceOptions = spaceOptions;

            EventSource = spaceOptions.Services.Get<Events.IEventSource>().ComponentEventSource;
        }

        protected IWebDriver WebDriver { get; }
        protected ISpaceOptions SpaceOptions { get; }

        protected Events.IComponentEventSource EventSource;
    }
}
