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
    public abstract class BasePageConditions<TSelf> : BaseConditions<TSelf>
    {
        public BasePageConditions(TimeSpan timeout, TimeSpan pollingInterval, IWebDriver webDriver, IElementHandlerRepository elementHandlerRepository, IElementLocator elementLocator, PageMetadata pageMetadata, IEventSource eventSource, ILogger logger)
            : base(timeout, pollingInterval)
        {
            WebDriver = webDriver;
            ElementHandlerRepository = elementHandlerRepository;
            ElementLocator = elementLocator;
            PageMetadata = pageMetadata;
            EventSource = eventSource;
            Logger = logger;
        }

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
        public virtual TSelf IsLoaded(TimeSpan? timeout = default)
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
                using (Logger.BeginLogScope($"Expect the {PageMetadata.Name} document state is complete"))
                {
                    Services.Waiter.Until(condition, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{PageMetadata.Name} page is not loaded yet. Current state is '{latestValue}'.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Various conditions for current page Url.
        /// </summary>
        public virtual UrlConditions<TSelf> Url
        {
            get
            {
                return new UrlConditions<TSelf>(WebDriver, _self, Timeout, PollingInterval, PageMetadata, Logger);
            }
        }

        /// <summary>
        /// Various conditions for current title of the page.
        /// </summary>
        public virtual TitleConditions<TSelf> Title
        {
            get
            {
                return new TitleConditions<TSelf>(WebDriver, _self, Timeout, PollingInterval, PageMetadata, Logger);
            }
        }

        /// <summary>
        /// Waits specified amount of time.
        /// </summary>
        /// <param name="duration">Aamount of time to wait.</param>
        /// <returns></returns>
        public virtual TSelf Elapsed(TimeSpan duration)
        {
            Thread.Sleep(duration);

            return _self;
        }
    }
}
