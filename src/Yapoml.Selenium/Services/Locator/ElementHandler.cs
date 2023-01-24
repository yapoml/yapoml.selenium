using OpenQA.Selenium;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events;

namespace Yapoml.Selenium.Services.Locator
{
    public class ElementHandler : IElementHandler
    {
        private readonly IWebDriver _webDriver;
        private readonly IElementHandler _parentElementHandler;
        private readonly IElementLocator _elementLocator;
        private readonly ComponentMetadata _componentMetadata;
        private readonly IEventSource _eventSource;

        public ElementHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, ComponentMetadata componentMetadata, IEventSource eventSource)
        {
            _webDriver = webDriver;
            _parentElementHandler = parentElementHandler;
            _elementLocator = elementLocator;
            By = by;
            _componentMetadata = componentMetadata;
            _eventSource = eventSource;
        }

        public ElementHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, IWebElement webElement, ComponentMetadata componentMetadata, IEventSource eventSource)
            : this(webDriver, parentElementHandler, elementLocator, by, componentMetadata, eventSource)
        {
            _webElement = webElement;
        }

        private IWebElement _webElement;

        public By By { get; }

        public IWebElement Locate()
        {
            if (_webElement == null)
            {
                ISearchContext searchContext;
                if (_parentElementHandler != null)
                {
                    searchContext = _parentElementHandler.Locate();
                }
                else
                {
                    searchContext = _webDriver;
                }

                _eventSource.ComponentEventSource.RaiseOnFindingComponent(_componentMetadata.Name, By);

                _webElement = _elementLocator.FindElement(searchContext, By);

                _eventSource.ComponentEventSource.RaiseOnFoundComponent(By, _webDriver, _webElement);
            }

            return _webElement;
        }

        IReadOnlyList<IWebElement> _webElements;

        public IReadOnlyList<IWebElement> LocateMany()
        {
            if (_webElements == null)
            {

                ISearchContext searchContext;
                if (_parentElementHandler != null)
                {
                    searchContext = _parentElementHandler.Locate();
                }
                else
                {
                    searchContext = _webDriver;
                }

                _eventSource.ComponentEventSource.RaiseOnFindingComponents(_componentMetadata.Name, By);

                _webElements = _elementLocator.FindElements(searchContext, By);

                _eventSource.ComponentEventSource.RaiseOnFoundComponents(By, _webElements);
            }

            return _webElements;
        }
    }
}
