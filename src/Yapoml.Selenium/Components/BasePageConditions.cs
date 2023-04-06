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

        public TConditions IsLoaded(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? Timeout;
            var actualPollingInterval = pollingInterval ?? PollingInterval;

            string latestValue = null;

            bool? condition()
            {
                latestValue = (WebDriver as IJavaScriptExecutor).ExecuteScript("return document.readyState;").ToString();

                if (latestValue.Equals("complete"))
                {
                    return true;
                }
                else
                {
                    return null;
                }
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException)
            {
                // TODO Put page name in exception
                throw Services.Waiter.BuildTimeoutException($"Page is not loaded yet. Current state is '{latestValue}'.", null, actualTimeout, actualPollingInterval, null);
            }

            return obj;
        }

        /// <summary>
        /// Various expected conditions for awaiting url.
        /// </summary>
        public UrlConditions<TConditions> Url
        {
            get
            {
                return new UrlConditions<TConditions>(WebDriver, obj, Timeout, PollingInterval);
            }
        }

        /// <summary>
        /// Various expected conditions for page title.
        /// </summary>
        public TitleConditions<TConditions> Title
        {
            get
            {
                return new TitleConditions<TConditions>(WebDriver, obj, Timeout, PollingInterval);
            }
        }
    }
}
