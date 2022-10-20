using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Events;

namespace Yapoml.Selenium.Components
{
    public abstract class BasePage
    {
        public BasePage(IWebDriver webDriver, ISpaceOptions spaceOptions)
        {
            WebDriver = webDriver;
            SpaceOptions = spaceOptions;

            EventSource = spaceOptions.Services.Get<IEventSource>().ComponentEventSource;
        }

        protected IWebDriver WebDriver { get; }

        protected ISpaceOptions SpaceOptions { get; }

        protected IComponentEventSource EventSource;
    }
}
