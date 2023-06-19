using OpenQA.Selenium;
using System;
using System.Threading;
using Yapoml.Selenium.Components.Conditions;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public abstract class BasePageConditions<TConditions> where TConditions : BasePageConditions<TConditions>
    {
        protected TConditions conditions;

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

        /// <summary>
        /// Evaluates document's state to be <c>complete</c> which means the page is fully loaded.
        /// It doesn't guarantee that some components on the page are present, they might be rendered dynamically.
        /// 
        /// If url is defined for the page, then it also evaluates current url.
        /// </summary>
        public virtual TConditions IsLoaded(TimeSpan? timeout = default)
        {
            timeout ??= Timeout;

            string latestValue = null;

            bool condition()
            {
                latestValue = (WebDriver as IJavaScriptExecutor).ExecuteScript("return document.readyState;").ToString();

                return latestValue.Equals("complete");
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, PollingInterval);
            }
            catch (TimeoutException ex)
            {
                // TODO Put page name in exception
                throw new TimeoutException($"Page is not loaded yet. Current state is '{latestValue}'.", ex);
            }

            return conditions;
        }

        /// <summary>
        /// Various conditions for current page Url.
        /// </summary>
        public virtual UrlConditions<TConditions> Url
        {
            get
            {
                return new UrlConditions<TConditions>(WebDriver, conditions, Timeout, PollingInterval);
            }
        }

        /// <summary>
        /// Various conditions for current title of the page.
        /// </summary>
        public virtual TitleConditions<TConditions> Title
        {
            get
            {
                return new TitleConditions<TConditions>(WebDriver, conditions, Timeout, PollingInterval);
            }
        }

        /// <summary>
        /// Waits specified amount of time.
        /// </summary>
        /// <param name="duration">Aamount of time to wait.</param>
        /// <returns></returns>
        public virtual TConditions Elapsed(TimeSpan duration)
        {
            Thread.Sleep(duration);

            return conditions;
        }
    }
}
