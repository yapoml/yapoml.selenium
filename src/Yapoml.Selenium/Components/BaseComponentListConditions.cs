using OpenQA.Selenium;
using System;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif
using System.Threading;
using Yapoml.Selenium.Components.Conditions;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public class BaseComponentListConditions<TListConditions, TComponentConditions>
            where TListConditions : BaseComponentListConditions<TListConditions, TComponentConditions>
            where TComponentConditions : BaseComponentConditions<TComponentConditions>
    {
        protected TListConditions listConditions;

        protected TimeSpan Timeout { get; }
        protected TimeSpan PollingInterval { get; }
        protected IWebDriver WebDriver { get; }
        protected IElementsListHandler ElementsListHandler { get; }
        protected IElementLocator ElementLocator { get; }
        protected IEventSource EventSource { get; }

        public BaseComponentListConditions(TimeSpan timeout, TimeSpan pollingInterval, IWebDriver webDriver, IElementsListHandler elementsListHandler, IElementLocator elementLocator, IEventSource eventSource)
        {
            Timeout = timeout;
            PollingInterval = pollingInterval;
            WebDriver = webDriver;
            ElementsListHandler = elementsListHandler;
            ElementLocator = elementLocator;
            EventSource = eventSource;
        }

        public CountCollectionConditions<TListConditions> Count => new CountCollectionConditions<TListConditions>(listConditions, ElementsListHandler, Timeout, PollingInterval);

#if NET6_0_OR_GREATER
        public TListConditions Each(Action<TComponentConditions> predicate, TimeSpan? timeout = default, [CallerArgumentExpression("predicate")] string predicateExpression = null)
#else
        public TListConditions Each(Action<TComponentConditions> predicate, TimeSpan? timeout = default)
#endif
        {
            timeout ??= Timeout;

            bool condition()
            {
                var elements = ElementsListHandler.LocateMany();

                for (int i = 0; i < elements.Count; i++)
                {
                    var elementHandler = new ElementHandler(WebDriver, null, ElementLocator, ElementsListHandler.By, elements[i], ElementsListHandler.ComponentsListMetadata.ComponentMetadata, ElementsListHandler.ElementHandlerRepository.CreateNestedRepository(), EventSource);
                    var elementCondition = (TComponentConditions)Activator.CreateInstance(typeof(TComponentConditions), TimeSpan.Zero, PollingInterval, WebDriver, elementHandler, ElementLocator, EventSource);

                    try
                    {
                        predicate(elementCondition);
                    }
                    catch (TimeoutException ex)
                    {
#if NET6_0_OR_GREATER
                        throw new TimeoutException($"The {i + 1}th {elementHandler.ComponentMetadata.Name} of {elements.Count} does not satisfy condition '{predicateExpression}'.", ex);
#else
                        throw new TimeoutException($"The {i + 1}th {elementHandler.ComponentMetadata.Name} of {elements.Count} does not satisfy condition.", ex);
#endif
                    }
                }

                return true;
            }

            try
            {
                Services.Waiter.Until(condition, timeout.Value, PollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"Not all {ElementsListHandler.ComponentsListMetadata.Name} satisfy condition.", ex);
            }

            return listConditions;
        }

        /// <summary>
        /// Waits specified amount of time.
        /// </summary>
        /// <param name="duration">Aamount of time to wait.</param>
        /// <returns></returns>
        public virtual TListConditions Elapsed(TimeSpan duration)
        {
            Thread.Sleep(duration);

            return listConditions;
        }
    }
}
