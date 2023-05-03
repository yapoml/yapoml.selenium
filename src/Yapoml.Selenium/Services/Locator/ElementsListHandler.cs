using OpenQA.Selenium;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events;

namespace Yapoml.Selenium.Services.Locator
{
    public class ElementsListHandler : IElementsListHandler
    {
        private readonly IWebDriver _webDriver;
        private readonly IElementHandler _parentElementHandler;
        private readonly IElementLocator _elementLocator;
        private readonly IEventSource _eventSource;

        public ElementsListHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, ComponentsListMetadata componentsListMetadata, IElementHandlerRepository elementHandlerRepository, IEventSource eventSource)
        {
            _webDriver = webDriver;
            _parentElementHandler = parentElementHandler;
            _elementLocator = elementLocator;
            By = by;
            ComponentsListMetadata = componentsListMetadata;
            ElementHandlerRepository = elementHandlerRepository;
            _eventSource = eventSource;
        }

        public By By { get; }

        public ComponentsListMetadata ComponentsListMetadata { get; }

        public IElementHandlerRepository ElementHandlerRepository { get; }

        public void Invalidate()
        {
            _webElements = null;

            foreach (var elementHandler in ElementHandlerRepository.ElementHandlers)
            {
                elementHandler.Invalidate();
            }
        }

        IReadOnlyList<IWebElement> _webElements;

        public IReadOnlyList<IWebElement> LocateMany()
        {
            if (_webElements == null)
            {
                if (_parentElementHandler != null)
                {
                    try
                    {
                        var parentElement = _parentElementHandler.Locate();

                        _eventSource.ComponentEventSource.RaiseOnFindingComponents(By, ComponentsListMetadata);

                        _webElements = _elementLocator.FindElements(parentElement, By);
                    }
                    catch (StaleElementReferenceException)
                    {
                        _parentElementHandler.Invalidate();

                        _webElements = _elementLocator.FindElements(_parentElementHandler.Locate(), By);
                    }
                }
                else
                {
                    _eventSource.ComponentEventSource.RaiseOnFindingComponents(By, ComponentsListMetadata);

                    _webElements = _elementLocator.FindElements(_webDriver, By);
                }

                _eventSource.ComponentEventSource.RaiseOnFoundComponents(By, _webElements, ComponentsListMetadata);
            }

            return _webElements;
        }
    }
}
