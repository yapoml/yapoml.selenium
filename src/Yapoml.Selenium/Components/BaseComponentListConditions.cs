using OpenQA.Selenium;
using System;
using System.Linq;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif
using System.Threading;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Components.Conditions;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public class BaseComponentListConditions<TSelf, TComponentConditions> : BaseConditions<TSelf>
    {
        public BaseComponentListConditions(TimeSpan timeout, TimeSpan pollingInterval, IWebDriver webDriver, IElementsListHandler elementsListHandler, IElementLocator elementLocator, IEventSource eventSource, ILogger logger)
            : base(timeout, pollingInterval)
        {
            WebDriver = webDriver;
            ElementsListHandler = elementsListHandler;
            ElementLocator = elementLocator;
            EventSource = eventSource;
            Logger = logger;
        }

        protected IWebDriver WebDriver { get; }
        protected IElementsListHandler ElementsListHandler { get; }
        protected IElementLocator ElementLocator { get; }
        protected IEventSource EventSource { get; }
        protected ILogger Logger { get; }

        public CountCollectionConditions<TSelf> Count => new CountCollectionConditions<TSelf>(_self, ElementsListHandler, Timeout, PollingInterval, Logger);

#if NET6_0_OR_GREATER
        public TSelf Each(Action<TComponentConditions> predicate, TimeSpan? timeout = default, [CallerArgumentExpression("predicate")] string predicateExpression = null)
#else
        public TSelf Each(Action<TComponentConditions> predicate, TimeSpan? timeout = default)
#endif
        {
            timeout ??= Timeout;

            bool condition()
            {
                var elements = ElementsListHandler.LocateMany();

                for (int i = 0; i < elements.Count; i++)
                {
                    var elementHandler = new ElementHandler(WebDriver, null, ElementLocator, ElementsListHandler.By, elements[i], ElementsListHandler.ComponentsListMetadata.ComponentMetadata, ElementsListHandler.ElementHandlerRepository.CreateNestedRepository(), EventSource);
                    var elementCondition = (TComponentConditions)Activator.CreateInstance(typeof(TComponentConditions), TimeSpan.FromMilliseconds(-1), PollingInterval, WebDriver, elementHandler, ElementLocator, EventSource, Logger);

                    try
                    {
                        predicate(elementCondition);
                    }
                    catch (TimeoutException ex)
                    {
                        var indexPostfix = (i + 1) switch
                        {
                            1 => "st",
                            2 => "nd",
                            3 => "rd",
                            _ => "th"
                        };
#if NET6_0_OR_GREATER
                        throw new ExpectException($"The {i + 1}{indexPostfix} {elementHandler.ComponentMetadata.Name} of {elements.Count} does not satisfy condition '{predicateExpression}'.", ex);
#else
                        throw new ExpectException($"The {i + 1}{indexPostfix} {elementHandler.ComponentMetadata.Name} of {elements.Count} does not satisfy condition.", ex);
#endif
                    }
                }

                return true;
            }

            try
            {
                using (var scope = Logger.BeginLogScope($"Expect each {ElementsListHandler.ComponentsListMetadata.ComponentMetadata.Name} satisfy conditions"))
                {
                    scope.Execute(() =>
                    {
                        Waiter.Until(condition, timeout.Value, PollingInterval);
                    });
                }
            }
            catch (TimeoutException ex)
            {
                throw new ExpectException($"Not all {ElementsListHandler.ComponentsListMetadata.Name} satisfy condition.", ex);
            }

            return _self;
        }

#if NET6_0_OR_GREATER
        public TSelf Contains(Action<TComponentConditions> predicate, TimeSpan? timeout = default, [CallerArgumentExpression("predicate")] string predicateExpression = null)
#else
        public TSelf Contains(Action<TComponentConditions> predicate, TimeSpan? timeout = default)
#endif
        {
            timeout ??= Timeout;

            bool condition()
            {
                var elements = ElementsListHandler.LocateMany();

                foreach (var element in elements)
                {
                    try
                    {
                        var elementHandler = new ElementHandler(WebDriver, null, ElementLocator, ElementsListHandler.By, element, ElementsListHandler.ComponentsListMetadata.ComponentMetadata, ElementsListHandler.ElementHandlerRepository.CreateNestedRepository(), EventSource);
                        var elementCondition = (TComponentConditions)Activator.CreateInstance(typeof(TComponentConditions), TimeSpan.FromMilliseconds(-1), PollingInterval, WebDriver, elementHandler, ElementLocator, EventSource);

                        predicate(elementCondition);

                        return true;
                    }
                    catch (TimeoutException)
                    {
                        return false;
                    }
                }

                return false;
            }

            try
            {
                Waiter.Until(condition, timeout.Value, PollingInterval);
            }
            catch (TimeoutException ex)
            {
#if NET6_0_OR_GREATER
                throw new ExpectException($"The {ElementsListHandler.ComponentsListMetadata.Name} does not contain any {ElementsListHandler.ComponentsListMetadata.ComponentMetadata.Name} satisfying condition '{predicateExpression}'.", ex);
#else
                throw new ExpectException($"The {ElementsListHandler.ComponentsListMetadata.Name} does not contain any {ElementsListHandler.ComponentsListMetadata.ComponentMetadata.Name} satisfying condition.", ex);
#endif
            }

            return _self;
        }

#if NET6_0_OR_GREATER
        public TSelf DoNotContain(Action<TComponentConditions> predicate, TimeSpan? timeout = default, [CallerArgumentExpression("predicate")] string predicateExpression = null)
#else
        public TSelf DoNotContain(Action<TComponentConditions> predicate, TimeSpan? timeout = default)
#endif
        {
            timeout ??= Timeout;

            bool condition()
            {
                var elements = ElementsListHandler.LocateMany();

                bool result = true;

                foreach (var element in elements)
                {
                    try
                    {
                        var elementHandler = new ElementHandler(WebDriver, null, ElementLocator, ElementsListHandler.By, element, ElementsListHandler.ComponentsListMetadata.ComponentMetadata, ElementsListHandler.ElementHandlerRepository.CreateNestedRepository(), EventSource);
                        var elementCondition = (TComponentConditions)Activator.CreateInstance(typeof(TComponentConditions), TimeSpan.FromMilliseconds(-1), PollingInterval, WebDriver, elementHandler, ElementLocator, EventSource);

                        predicate(elementCondition);

                        // this one still satisfy condition, so returning false for reiterating
                        result = false;
                    }
                    catch (TimeoutException)
                    {
                        // do noting and leave result true b default, proceding next item
                    }
                }

                return result;
            }

            try
            {
                Waiter.Until(condition, timeout.Value, PollingInterval);
            }
            catch (TimeoutException ex)
            {
#if NET6_0_OR_GREATER
                throw new ExpectException($"The {ElementsListHandler.ComponentsListMetadata.Name} contain at least one {ElementsListHandler.ComponentsListMetadata.ComponentMetadata.Name} satisfying condition '{predicateExpression}'.", ex);
#else
                throw new ExpectException($"The {ElementsListHandler.ComponentsListMetadata.Name} contain at least one {ElementsListHandler.ComponentsListMetadata.ComponentMetadata.Name} satisfying condition.", ex);
#endif
            }

            return _self;
        }

        public virtual TSelf IsEmpty(TimeSpan? timeout = default)
        {
            return Count.Is(0, timeout);
        }

        public virtual TSelf IsNotEmpty(TimeSpan? timeout = default)
        {
            return Count.IsGreaterThan(0, timeout);
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
