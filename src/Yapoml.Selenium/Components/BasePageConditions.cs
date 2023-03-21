using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Components.Conditions;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public abstract class BasePageConditions<TConditions> where TConditions : BasePageConditions<TConditions>
    {
        protected TConditions obj;

        public BasePageConditions(TimeSpan timeout, TimeSpan pollingInterval, IWebDriver webDriver, IElementHandlerRepository elementHandlerRepository, IElementLocator elementLocator, IEventSource eventSource)
        {
            Timeout = timeout;
            PollingInterval = pollingInterval;
            WebDriver = webDriver;
            ElementHandlerRepository = elementHandlerRepository;
            ElementLocator = elementLocator;
            EventSource = eventSource;
        }

        protected TimeSpan Timeout { get; }
        protected TimeSpan PollingInterval { get; }
        protected IWebDriver WebDriver { get; }
        protected IElementHandlerRepository ElementHandlerRepository { get; }
        protected IElementLocator ElementLocator { get; }
        protected IEventSource EventSource { get; }

        public TConditions IsLoaded()
        {
            throw new NotImplementedException();

            return obj;
        }

        /// <summary>
        /// Various conditions for awaiting url.
        /// </summary>
        public UrlConditions<TConditions> Url
        {
            get
            {
                return new UrlConditions<TConditions>(WebDriver, obj, Timeout, PollingInterval);
            }
        }
    }
}
