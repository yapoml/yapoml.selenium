using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Events;

namespace Yapoml.Selenium.Components
{
    public abstract class BaseComponent
    {
        protected IWebDriver WebDriver { get; private set; }

        public IWebElement WrappedElement { get; private set; }

        protected ISpaceOptions SpaceOptions { get; private set; }

        protected IComponentEventSource EventSource { get; private set; }

        public BaseComponent(IWebDriver webDriver, IWebElement webElement, ISpaceOptions spaceOptions)
        {
            WebDriver = webDriver;
            WrappedElement = webElement;
            SpaceOptions = spaceOptions;

            EventSource = spaceOptions.Services.Get<IEventSource>().ComponentEventSource;
        }
    }
}