using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public abstract class BaseComponentConditions<TConditions> where TConditions : BaseComponentConditions<TConditions>
    {
        protected TConditions obj;

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

            return obj;
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

            return obj;
        }

        /// <summary>
        /// Waits specific attribute value.
        /// </summary>
        /// <param name="name">Attribute name.</param>
        /// <param name="value">Expected value of the attribute.</param>
        /// <param name="timeout">How long to wait until attribute value becomes expected.</param>
        /// <param name="pollingInterval">Interval between verifications in a loop.</param>
        /// <returns></returns>
        public virtual TConditions AttributeIs(string name, string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            Services.Waiter.UntilAttributeValue(ElementHandler, name, value, timeout ?? Timeout, pollingInterval ?? PollingInterval);
            return obj;
        }

        /// <summary>
        /// Waits specific CSS value.
        /// </summary>
        /// <param name="name">CSS property name.</param>
        /// <param name="value">Expected value of the CSS property.</param>
        /// <param name="timeout">How long to wait until CSS value becomes expected.</param>
        /// <param name="pollingInterval">Interval between verifications in a loop.</param>
        /// <returns></returns>
        public virtual TConditions CssIs(string name, string value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            Services.Waiter.UntilCssValue(ElementHandler, name, value, timeout ?? Timeout, pollingInterval ?? PollingInterval);

            return obj;
        }

    }
}
