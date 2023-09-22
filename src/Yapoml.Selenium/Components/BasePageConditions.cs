using OpenQA.Selenium;
using System;
using System.Threading;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Components.Conditions;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public abstract class BasePageConditions<TConditions> where TConditions : BasePageConditions<TConditions>
    {
        protected TConditions conditions;

        public BasePageConditions(TimeSpan timeout, TimeSpan pollingInterval, IWebDriver webDriver, IElementHandlerRepository elementHandlerRepository, IElementLocator elementLocator, PageMetadata pageMetadata, IEventSource eventSource, ILogger logger)
        {
            Timeout = timeout;
            PollingInterval = pollingInterval;
            WebDriver = webDriver;
            ElementHandlerRepository = elementHandlerRepository;
            ElementLocator = elementLocator;
            PageMetadata = pageMetadata;
            EventSource = eventSource;
            Logger = logger;
        }

        protected TimeSpan Timeout { get; }
        protected TimeSpan PollingInterval { get; }
        protected IWebDriver WebDriver { get; }
        protected IElementHandlerRepository ElementHandlerRepository { get; }
        protected IElementLocator ElementLocator { get; }
        protected PageMetadata PageMetadata { get; }
        protected IEventSource EventSource { get; }
        protected ILogger Logger { get; }

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
                using (Logger.BeginLogScope($"Expect the {PageMetadata.Name} page is loaded"))
                {
                    Services.Waiter.Until(condition, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{PageMetadata.Name} page is not loaded yet. Current state is '{latestValue}'.", ex);
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
                return new UrlConditions<TConditions>(WebDriver, conditions, Timeout, PollingInterval, PageMetadata, Logger);
            }
        }

        /// <summary>
        /// Various conditions for current title of the page.
        /// </summary>
        public virtual TitleConditions<TConditions> Title
        {
            get
            {
                return new TitleConditions<TConditions>(WebDriver, conditions, Timeout, PollingInterval, PageMetadata, Logger);
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
