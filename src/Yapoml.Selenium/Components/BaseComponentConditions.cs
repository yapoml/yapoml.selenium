using OpenQA.Selenium;
using System;
using System.Threading;
using Yapoml.Selenium.Components.Conditions;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public abstract class BaseComponentConditions<TConditions> where TConditions : BaseComponentConditions<TConditions>
    {
        protected TConditions conditions;

        protected TimeSpan Timeout { get; }
        protected TimeSpan PollingInterval { get; }
        protected IWebDriver WebDriver { get; }
        protected IElementHandler ElementHandler { get; }
        protected IElementLocator ElementLocator { get; }
        protected IEventSource EventSource { get; }

        public BaseComponentConditions(TimeSpan timeout, TimeSpan pollingInterval, IWebDriver webDriver, IElementHandler elementHandler, IElementLocator elementLocator, IEventSource eventSource)
        {
            Timeout = timeout;
            PollingInterval = pollingInterval;
            WebDriver = webDriver;
            ElementHandler = elementHandler;
            ElementLocator = elementLocator;
            EventSource = eventSource;
        }

        /// <summary>
        /// Waits until the component is displayed.
        /// </summary>
        /// <param name="timeout">How long to wait until the component component is displayed.</param>
        /// <param name="pollingInterval">Interval between verifications in a loop.</param>
        /// <returns></returns>
        public virtual TConditions IsDisplayed(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            Services.Waiter.UntilDisplayed(ElementHandler, timeout ?? Timeout, pollingInterval ?? PollingInterval);

            return conditions;
        }

        /// <summary>
        ///  Waits until the component is enabled.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is enabled.</param>
        /// <param name="pollingInterval">Interval between verifications in a loop.</param>
        /// <returns></returns>
        public virtual TConditions IsEnabled(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            Services.Waiter.UntilEnabled(ElementHandler, timeout ?? Timeout, pollingInterval ?? PollingInterval);

            return conditions;
        }

        /// <summary>
        /// Various expected conditions for component's text.
        /// </summary>
        public virtual TextConditions<TConditions> Text
        {
            get
            {
                return new TextConditions<TConditions>(conditions, ElementHandler, Timeout, PollingInterval);
            }
        }

        /// <summary>
        /// Various expected conditions for component attributes.
        /// </summary>
        public virtual AttributesCollectionConditions<TConditions> Attributes
        {
            get
            {
                return new AttributesCollectionConditions<TConditions>(conditions, ElementHandler, Timeout, PollingInterval);
            }
        }

        /// <summary>
        /// Various expected conditions for CSS styles.
        /// </summary>
        public virtual StylesCollectionConditions<TConditions> Styles
        {
            get
            {
                return new StylesCollectionConditions<TConditions>(conditions, ElementHandler, Timeout, PollingInterval);
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
