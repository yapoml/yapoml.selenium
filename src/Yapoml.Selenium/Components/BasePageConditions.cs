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
        /// Waits until current <c>document.readyState</c> is <c>complete</c>.
        /// </summary>
        public virtual TConditions IsLoaded()
        {
            return IsLoaded(Timeout);
        }

        /// <summary>
        /// Waits until current <c>document.readyState</c> is <c>complete</c>.
        /// </summary>
        public virtual TConditions IsLoaded(TimeSpan timeout)
        {
            string latestValue = null;

            bool condition()
            {
                latestValue = (WebDriver as IJavaScriptExecutor).ExecuteScript("return document.readyState;").ToString();

                return latestValue.Equals("complete");
            }

            try
            {
                Services.Waiter.Until(condition, timeout, PollingInterval);
            }
            catch (TimeoutException ex)
            {
                // TODO Put page name in exception
                throw new TimeoutException($"Page is not loaded yet. Current state is '{latestValue}'.", ex);
            }

            return conditions;
        }

        /// <summary>
        /// Various expected conditions for awaiting url.
        /// </summary>
        public virtual UrlConditions<TConditions> Url
        {
            get
            {
                return new UrlConditions<TConditions>(WebDriver, conditions, Timeout, PollingInterval);
            }
        }

        /// <summary>
        /// Various expected conditions for page title.
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
