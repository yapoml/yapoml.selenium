using OpenQA.Selenium;
using System;
using System.Threading;
using Yapoml.Selenium.Components.Conditions;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public class BaseComponentListConditions<TListConditions> where TListConditions : BaseComponentListConditions<TListConditions>
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
