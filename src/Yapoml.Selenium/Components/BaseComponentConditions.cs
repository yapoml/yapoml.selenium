using OpenQA.Selenium;
using System;
using System.Collections.Generic;
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
        /// <param name="timeout">How long to wait until the component is displayed.</param>
        /// <param name="pollingInterval">Interval between verifications in a loop.</param>
        /// <returns></returns>
        public virtual TConditions IsDisplayed(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            timeout = timeout ?? Timeout;
            pollingInterval = pollingInterval ?? PollingInterval;

            Exception lastError = null;

            Dictionary<Type, uint> ignoredExceptions = new Dictionary<Type, uint> {
                { typeof(NoSuchElementException), 0 },
                { typeof(StaleElementReferenceException), 0 }
             };

            bool attempt()
            {
                try
                {
                    var element = ElementHandler.Locate();

                    if (element.Displayed)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex) when (ignoredExceptions.ContainsKey(ex.GetType()))
                {
                    if (ex is StaleElementReferenceException)
                    {
                        ElementHandler.Invalidate();
                    }

                    lastError = ex;

                    ignoredExceptions[ex.GetType()]++;

                    return false;
                }
            }

            try
            {
                Services.Waiter.Until(attempt, timeout.Value, pollingInterval.Value);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"{ElementHandler.ComponentMetadata.Name} is not displayed yet '{ElementHandler.By}'.", ex);
            }

            return conditions;
        }

        /// <summary>
        /// Waits until the component is not displayed.
        /// Detached component from DOM is also considered as not displayed.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is not displayed.</param>
        /// <param name="pollingInterval">Interval between verifications in a loop.</param>
        /// <returns></returns>
        public virtual TConditions IsNotDisplayed(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            timeout = timeout ?? Timeout;
            pollingInterval = pollingInterval ?? PollingInterval;

            bool attempt()
            {
                try
                {
                    var element = ElementHandler.Locate();

                    return !element.Displayed;
                }
                catch (Exception ex) when (ex is StaleElementReferenceException || ex is NoSuchElementException)
                {
                    if (ex is StaleElementReferenceException)
                    {
                        ElementHandler.Invalidate();
                    }

                    return true;
                }
            }

            try
            {
                Services.Waiter.Until(attempt, timeout.Value, pollingInterval.Value);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"{ElementHandler.ComponentMetadata.Name} is still displayed '{ElementHandler.By}'.", ex);
            }

            return conditions;
        }

        /// <summary>
        /// Waits until the component is appeared in the DOM.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is appeared.</param>
        /// <param name="pollingInterval">Interval between verifications in a loop.</param>
        /// <returns></returns>
        public virtual TConditions Exists(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            timeout = timeout ?? Timeout;
            pollingInterval = pollingInterval ?? PollingInterval;

            Exception lastError = null;

            Dictionary<Type, uint> ignoredExceptions = new Dictionary<Type, uint> {
                { typeof(NoSuchElementException), 0 },
                { typeof(StaleElementReferenceException), 0 }
             };

            bool attempt()
            {
                try
                {
                    var element = ElementHandler.Locate();

                    // ping element
                    var tagName = element.TagName;

                    return true;
                }
                catch (Exception ex) when (ignoredExceptions.ContainsKey(ex.GetType()))
                {
                    if (ex is StaleElementReferenceException)
                    {
                        ElementHandler.Invalidate();
                    }

                    lastError = ex;

                    ignoredExceptions[ex.GetType()]++;

                    return false;
                }
            }

            try
            {
                Services.Waiter.Until(attempt, timeout.Value, pollingInterval.Value);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"{ElementHandler.ComponentMetadata.Name} does not exist yet '{ElementHandler.By}'.", ex);
            }

            return conditions;
        }

        /// <summary>
        /// Waits until the component disappeared drom the DOM.
        /// </summary>
        /// <param name="timeout">How long to wait until the component disappeared.</param>
        /// <param name="pollingInterval">Interval between verifications in a loop.</param>
        /// <returns></returns>
        public virtual TConditions DoesNotExist(TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            timeout = timeout ?? Timeout;
            pollingInterval = pollingInterval ?? PollingInterval;

            bool attempt()
            {
                try
                {
                    var element = ElementHandler.Locate();

                    // ping element
                    var tagName = element.TagName;

                    return false;
                }
                catch (Exception ex) when (ex is StaleElementReferenceException || ex is NoSuchElementException)
                {
                    if (ex is StaleElementReferenceException)
                    {
                        ElementHandler.Invalidate();
                    }

                    return true;
                }
            }

            try
            {
                Services.Waiter.Until(attempt, timeout.Value, pollingInterval.Value);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"{ElementHandler.ComponentMetadata.Name} still exists '{ElementHandler.By}'.", ex);
            }

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
            timeout = timeout ?? Timeout;
            pollingInterval = pollingInterval ?? PollingInterval;

            bool attempt()
            {
                return ElementHandler.Locate().Enabled;
            }

            try
            {
                Services.Waiter.Until(attempt, timeout.Value, pollingInterval.Value);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"{ElementHandler.ComponentMetadata.Name} is not enabled yet.", ex);
            }

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
