﻿using OpenQA.Selenium;
using System;
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

        public ElementsListHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, ElementLocatorContext from, ComponentsListMetadata componentsListMetadata, IElementHandlerRepository elementHandlerRepository, IEventSource eventSource)
        {
            _webDriver = webDriver;
            _parentElementHandler = parentElementHandler;
            _elementLocator = elementLocator;
            By = by;
            From = from;
            ComponentsListMetadata = componentsListMetadata;
            ElementHandlerRepository = elementHandlerRepository;
            _eventSource = eventSource;
        }

        public By By { get; }

        public ElementLocatorContext From { get; }

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
                if (From == ElementLocatorContext.Parent)
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
                }
                else if (From == ElementLocatorContext.Root)
                {
                    _eventSource.ComponentEventSource.RaiseOnFindingComponents(By, ComponentsListMetadata);

                    _webElements = _elementLocator.FindElements(_webDriver, By);
                }
                else
                {
                    throw new NotImplementedException($"Element locator context {From} is not supported yet.");
                }

                _eventSource.ComponentEventSource.RaiseOnFoundComponents(By, _webDriver, _webElements, ComponentsListMetadata);
            }

            return _webElements;
        }
    }
}
