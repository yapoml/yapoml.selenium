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
        private readonly IEventSource _eventSource;

        public ElementHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, ComponentMetadata componentMetadata, IEventSource eventSource)
        {
            _webDriver = webDriver;
            _parentElementHandler = parentElementHandler;
            _elementLocator = elementLocator;
            By = by;
            ComponentMetadata = componentMetadata;
            _eventSource = eventSource;
        }

        public ElementHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, IWebElement webElement, ComponentMetadata componentMetadata, IEventSource eventSource)
            : this(webDriver, parentElementHandler, elementLocator, by, componentMetadata, eventSource)
        {
            _webElement = webElement;
        }

        private IWebElement _webElement;

        public By By { get; }

        public ComponentMetadata ComponentMetadata { get; }

        public IWebElement Locate()
        {
            if (_webElement == null)
            {
                _eventSource.ComponentEventSource.RaiseOnFindingComponent(ComponentMetadata.Name, By);

                if (_parentElementHandler != null)
                {
                    try
                    {
                        _webElement = _elementLocator.FindElement(_parentElementHandler.Locate(), By);
                    }
                    catch (StaleElementReferenceException)
                    {
                        _parentElementHandler.Invalidate();

                        _webElement = _elementLocator.FindElement(_parentElementHandler.Locate(), By);
                    }
                }
                else
                {
                    _webElement = _elementLocator.FindElement(_webDriver, By);
                }

                _eventSource.ComponentEventSource.RaiseOnFoundComponent(By, _webDriver, _webElement);
            }

            return _webElement;
        }

        public void Invalidate()
        {
            _webElement = null;

            _webElements = null;
        }

        IReadOnlyList<IWebElement> _webElements;

        public IReadOnlyList<IWebElement> LocateMany()
        {
            if (_webElements == null)
            {
                _eventSource.ComponentEventSource.RaiseOnFindingComponents(ComponentMetadata.Name, By);

                if (_parentElementHandler != null)
                {
                    try
                    {
                        _webElements = _elementLocator.FindElements(_parentElementHandler.Locate(), By);
                    }
                    catch (StaleElementReferenceException)
                    {
                        _parentElementHandler.Invalidate();

                        _webElements = _elementLocator.FindElements(_parentElementHandler.Locate(), By);
                    }
                }
                else
                {
                    _webElements = _elementLocator.FindElements(_webDriver, By);
                }

                _eventSource.ComponentEventSource.RaiseOnFoundComponents(By, _webElements);
            }

            return _webElements;
        }
    }
}
