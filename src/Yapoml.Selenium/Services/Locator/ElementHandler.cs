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

        public ElementHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, ComponentMetadata componentMetadata, IElementHandlerRepository elementHandlerRepository, IEventSource eventSource)
        {
            _webDriver = webDriver;
            _parentElementHandler = parentElementHandler;
            _elementLocator = elementLocator;
            By = by;
            ComponentMetadata = componentMetadata;
            ElementHandlerRepository = elementHandlerRepository;
            _eventSource = eventSource;
        }

        public ElementHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, IWebElement webElement, ComponentMetadata componentMetadata, IElementHandlerRepository elementHandlerRepository, IEventSource eventSource)
            : this(webDriver, parentElementHandler, elementLocator, by, componentMetadata, elementHandlerRepository, eventSource)
        {
            _webElement = webElement;
        }

        private IWebElement _webElement;

        public By By { get; }

        public ComponentMetadata ComponentMetadata { get; }

        public IElementHandlerRepository ElementHandlerRepository { get; }

        public IWebElement Locate()
        {
            if (_webElement == null)
            {
                if (_parentElementHandler != null)
                {
                    try
                    {
                        var parentElement = _parentElementHandler.Locate();

                        _eventSource.ComponentEventSource.RaiseOnFindingComponent(By, ComponentMetadata);

                        _webElement = _elementLocator.FindElement(parentElement, By);
                    }
                    catch (StaleElementReferenceException)
                    {
                        _parentElementHandler.Invalidate();

                        var parentElement = _parentElementHandler.Locate();

                        _webElement = _elementLocator.FindElement(parentElement, By);
                    }
                }
                else
                {
                    _eventSource.ComponentEventSource.RaiseOnFindingComponent(By, ComponentMetadata);

                    _webElement = _elementLocator.FindElement(_webDriver, By);
                }

                _eventSource.ComponentEventSource.RaiseOnFoundComponent(By, _webDriver, _webElement, ComponentMetadata);
            }

            return _webElement;
        }

        public void Invalidate()
        {
            _webElement = null;

            foreach (var elementHandler in ElementHandlerRepository.ElementHandlers)
            {
                elementHandler.Invalidate();
            }
        }
    }
}
