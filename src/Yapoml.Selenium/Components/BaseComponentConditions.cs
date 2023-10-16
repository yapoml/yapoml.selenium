using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Components.Conditions;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public abstract class BaseComponentConditions<TSelf> : BaseConditions<TSelf>, ITextualConditions<TSelf>
    {
        public BaseComponentConditions(TimeSpan timeout, TimeSpan pollingInterval, IWebDriver webDriver, IElementHandler elementHandler, IElementLocator elementLocator, IEventSource eventSource, ILogger logger)
            : base(timeout, pollingInterval)
        {
            WebDriver = webDriver;
            ElementHandler = elementHandler;
            ElementLocator = elementLocator;
            EventSource = eventSource;
            Logger = logger;
        }

        protected IWebDriver WebDriver { get; }
        protected IElementHandler ElementHandler { get; }
        protected IElementLocator ElementLocator { get; }
        protected IEventSource EventSource { get; }
        protected ILogger Logger { get; }

        /// <summary>
        /// Waits until the component is displayed.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is displayed.</param>
        /// <returns></returns>
        public virtual TSelf IsDisplayed(TimeSpan? timeout = null)
        {
            timeout ??= Timeout;

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
                using (Logger.BeginLogScope($"Expect {ElementHandler.ComponentMetadata.Name} is displayed"))
                {
                    Services.Waiter.Until(attempt, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{ElementHandler.ComponentMetadata.Name} is not displayed yet '{ElementHandler.By}'.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Waits until the component is not displayed.
        /// Detached component from DOM is also considered as not displayed.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is not displayed.</param>
        /// <returns></returns>
        public virtual TSelf IsNotDisplayed(TimeSpan? timeout = null)
        {
            timeout ??= Timeout;

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
                using (Logger.BeginLogScope($"Expect {ElementHandler.ComponentMetadata.Name} is not displayed"))
                {
                    Services.Waiter.Until(attempt, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{ElementHandler.ComponentMetadata.Name} is still displayed '{ElementHandler.By}'.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Waits until the component is appeared in the DOM.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is appeared.</param>
        /// <returns></returns>
        public virtual TSelf Exists(TimeSpan? timeout = null)
        {
            timeout ??= Timeout;

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
                using (Logger.BeginLogScope($"Expect {ElementHandler.ComponentMetadata.Name} exists"))
                {
                    Services.Waiter.Until(attempt, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{ElementHandler.ComponentMetadata.Name} does not exist yet '{ElementHandler.By}'.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Waits until the component disappeared drom the DOM.
        /// </summary>
        /// <param name="timeout">How long to wait until the component disappeared.</param>
        /// <returns></returns>
        public virtual TSelf DoesNotExist(TimeSpan? timeout = null)
        {
            timeout ??= Timeout;

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
                using (Logger.BeginLogScope($"Expect {ElementHandler.ComponentMetadata.Name} does not exist"))
                {
                    Services.Waiter.Until(attempt, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{ElementHandler.ComponentMetadata.Name} still exists '{ElementHandler.By}'.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Waits until the component is enabled.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is enabled.</param>
        /// <returns></returns>
        public virtual TSelf IsEnabled(TimeSpan? timeout = null)
        {
            timeout ??= Timeout;

            bool attempt()
            {
                return ElementHandler.Locate().Enabled;
            }

            try
            {
                using (Logger.BeginLogScope($"Expect {ElementHandler.ComponentMetadata.Name} is enabled"))
                {
                    Services.Waiter.Until(attempt, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{ElementHandler.ComponentMetadata.Name} is not enabled yet.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Waits until the component is disabled.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is disabled.</param>
        /// <returns></returns>
        public virtual TSelf IsDisabled(TimeSpan? timeout = null)
        {
            timeout ??= Timeout;

            bool attempt()
            {
                return !ElementHandler.Locate().Enabled;
            }

            try
            {
                using (Logger.BeginLogScope($"Expect {ElementHandler.ComponentMetadata.Name} is disabled"))
                {
                    Services.Waiter.Until(attempt, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{ElementHandler.ComponentMetadata.Name} is still enabled.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Waits until the component is disabled.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is disabled.</param>
        /// <returns></returns>
        public TSelf IsNotEnabled(TimeSpan? timeout = null)
        {
            return IsDisabled(timeout);
        }

        /// <summary>
        /// Waits until the component is in view.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is in view.</param>
        /// <returns></returns>
        public virtual TSelf IsInView(TimeSpan? timeout = null)
        {
            timeout ??= Timeout;

            var js = @"
const rect = arguments[0].getBoundingClientRect();
return (
    rect.top >= 0 &&
    rect.left >= 0 &&
    rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
    rect.right <= (window.innerWidth || document.documentElement.clientWidth)
);";

            var jsExecutor = WebDriver as IJavaScriptExecutor;

            bool attempt()
            {
                return (bool)jsExecutor.ExecuteScript(js, ElementHandler.Locate());
            }

            try
            {
                using (Logger.BeginLogScope($"Expect {ElementHandler.ComponentMetadata.Name} is in view"))
                {
                    Services.Waiter.Until(attempt, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{ElementHandler.ComponentMetadata.Name} is not in view yet.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Waits until the component is not in view.
        /// </summary>
        /// <param name="timeout">How long to wait until the component is not in view.</param>
        /// <returns></returns>
        public virtual TSelf IsNotInView(TimeSpan? timeout = null)
        {
            timeout ??= Timeout;

            var js = @"
const rect = arguments[0].getBoundingClientRect();
return (
    rect.top >= 0 &&
    rect.left >= 0 &&
    rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
    rect.right <= (window.innerWidth || document.documentElement.clientWidth)
);";

            var jsExecutor = WebDriver as IJavaScriptExecutor;

            bool attempt()
            {
                return !(bool)jsExecutor.ExecuteScript(js, ElementHandler.Locate());
            }

            try
            {
                using (Logger.BeginLogScope($"Expect {ElementHandler.ComponentMetadata.Name} is not in view"))
                {
                    Services.Waiter.Until(attempt, timeout.Value, PollingInterval);
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"{ElementHandler.ComponentMetadata.Name} is still in view.", ex);
            }

            return _self;
        }

        /// <summary>
        /// Various expected conditions for component's text.
        /// </summary>
        public virtual TextConditions<TSelf> Text
        {
            get
            {
                return new TextConditions<TSelf>(_self, ElementHandler, Timeout, PollingInterval, $"text of the {ElementHandler.ComponentMetadata.Name}", Logger);
            }
        }

        /// <summary>
        /// Various expected conditions for component attributes.
        /// </summary>
        public virtual AttributesCollectionConditions<TSelf> Attributes
        {
            get
            {
                return new AttributesCollectionConditions<TSelf>(_self, ElementHandler, Timeout, PollingInterval, Logger);
            }
        }

        /// <summary>
        /// Various expected conditions for CSS styles.
        /// </summary>
        public virtual StylesCollectionConditions<TSelf> Styles
        {
            get
            {
                return new StylesCollectionConditions<TSelf>(_self, ElementHandler, Timeout, PollingInterval, Logger);
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

        #region Textual Conditions

        public TSelf Is(string value, TimeSpan? timeout = default)
        {
            return Text.Is(value, timeout);
        }

        public TSelf Is(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            return Text.Is(value, comparisonType, timeout);
        }

        public TSelf IsNot(string value, TimeSpan? timeout = default)
        {
            return Text.IsNot(value, timeout);
        }

        public TSelf IsNot(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            return Text.IsNot(value, comparisonType, timeout);
        }

        public TSelf IsEmpty(TimeSpan? timeout = default)
        {
            return Text.IsEmpty(timeout);
        }

        public TSelf IsNotEmpty(TimeSpan? timeout = default)
        {
            return Text.IsNotEmpty(timeout);
        }

        public TSelf StartsWith(string value, TimeSpan? timeout = default)
        {
            return Text.StartsWith(value, timeout);
        }

        public TSelf StartsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            return Text.StartsWith(value, comparisonType, timeout);
        }

        public TSelf DoesNotStartWith(string value, TimeSpan? timeout = default)
        {
            return Text.DoesNotStartWith(value, timeout);
        }

        public TSelf DoesNotStartWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            return Text.DoesNotStartWith(value, comparisonType, timeout);
        }

        public TSelf EndsWith(string value, TimeSpan? timeout = default)
        {
            return Text.EndsWith(value, timeout);
        }

        public TSelf EndsWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            return Text.EndsWith(value, comparisonType, timeout);
        }

        public TSelf DoesNotEndWith(string value, TimeSpan? timeout = default)
        {
            return Text.DoesNotEndWith(value, timeout);
        }

        public TSelf DoesNotEndWith(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            return Text.DoesNotEndWith(value, comparisonType, timeout);
        }

        public TSelf Contains(string value, TimeSpan? timeout = default)
        {
            return Text.Contains(value, timeout);
        }

        public TSelf Contains(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            return Text.Contains(value, comparisonType, timeout);
        }

        public TSelf DoesNotContain(string value, TimeSpan? timeout = default)
        {
            return Text.DoesNotContain(value, timeout);
        }

        public TSelf DoesNotContain(string value, StringComparison comparisonType, TimeSpan? timeout = default)
        {
            return Text.DoesNotContain(value, comparisonType, timeout);
        }

        public TSelf Matches(Regex regex, TimeSpan? timeout = default)
        {
            return Text.Matches(regex, timeout);
        }

        public TSelf DoesNotMatch(Regex regex, TimeSpan? timeout = default)
        {
            return Text.DoesNotMatch(regex, timeout);
        }
        #endregion
    }
}
