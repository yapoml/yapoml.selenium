using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;
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

        public ElementHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, ElementLocatorContext from, ComponentMetadata componentMetadata, IElementHandlerRepository elementHandlerRepository, IEventSource eventSource)
        {
            _webDriver = webDriver;
            _parentElementHandler = parentElementHandler;
            _elementLocator = elementLocator;
            By = by;
            From = from;
            ComponentMetadata = componentMetadata;
            ElementHandlerRepository = elementHandlerRepository;
            _eventSource = eventSource;
        }

        public ElementHandler(IWebDriver webDriver, IElementHandler parentElementHandler, IElementLocator elementLocator, By by, ElementLocatorContext from, IWebElement webElement, ComponentMetadata componentMetadata, IElementHandlerRepository elementHandlerRepository, IEventSource eventSource)
            : this(webDriver, parentElementHandler, elementLocator, by, from, componentMetadata, elementHandlerRepository, eventSource)
        {
            _webElement = webElement;
        }

        private IWebElement _webElement;

        public By By { get; }

        public ElementLocatorContext From { get; }

        public ComponentMetadata ComponentMetadata { get; }

        public IElementHandlerRepository ElementHandlerRepository { get; }

        public IWebElement Locate()
        {
            return Locate(TimeSpan.Zero, TimeSpan.Zero);
        }

        public IWebElement Locate(TimeSpan timeout, TimeSpan pollingInterval)
        {
            if (_webElement == null)
            {
                if (From == ElementLocatorContext.Parent)
                {
                    if (_parentElementHandler != null)
                    {
                        try
                        {
                            var parentElement = _parentElementHandler.Locate(timeout, pollingInterval);

                            _eventSource.ComponentEventSource.RaiseOnFindingComponent(By, ComponentMetadata);

                            var stopwatch = Stopwatch.StartNew();

                            Exception lastException = null;

                            do
                            {
                                try
                                {
                                    _webElement = _elementLocator.FindElement(parentElement, By);

                                    break;
                                }
                                catch (NoSuchElementException exp)
                                {
                                    lastException = exp;
                                    Thread.Sleep(pollingInterval);
                                }
                            }
                            while (stopwatch.Elapsed <= timeout);

                            if (_webElement is null)
                            {
                                throw lastException;
                            }
                        }
                        catch (StaleElementReferenceException)
                        {
                            _parentElementHandler.Invalidate();

                            var parentElement = _parentElementHandler.Locate(timeout, pollingInterval);

                            var stopwatch = Stopwatch.StartNew();

                            Exception lastException = null;

                            do
                            {
                                try
                                {
                                    _webElement = _elementLocator.FindElement(parentElement, By);

                                    break;
                                }
                                catch (NoSuchElementException exp)
                                {
                                    lastException = exp;
                                    Thread.Sleep(pollingInterval);
                                }
                            }
                            while (stopwatch.Elapsed <= timeout);

                            if (_webElement is null)
                            {
                                throw lastException;
                            }
                        }
                    }
                    else
                    {
                        _eventSource.ComponentEventSource.RaiseOnFindingComponent(By, ComponentMetadata);

                        var stopwatch = Stopwatch.StartNew();

                        Exception lastException = null;

                        do
                        {
                            try
                            {
                                _webElement = _elementLocator.FindElement(_webDriver, By);

                                break;
                            }
                            catch (NoSuchElementException exp)
                            {
                                lastException = exp;
                                Thread.Sleep(pollingInterval);
                            }
                        }
                        while (stopwatch.Elapsed <= timeout);

                        if (_webElement is null)
                        {
                            throw lastException;
                        }
                    }
                }
                else if (From == ElementLocatorContext.Root)
                {
                    _eventSource.ComponentEventSource.RaiseOnFindingComponent(By, ComponentMetadata);

                    var stopwatch = Stopwatch.StartNew();

                    Exception lastException = null;

                    do
                    {
                        try
                        {
                            _webElement = _elementLocator.FindElement(_webDriver, By);

                            break;
                        }
                        catch (NoSuchElementException exp)
                        {
                            lastException = exp;
                            Thread.Sleep(pollingInterval);
                        }
                    }
                    while (stopwatch.Elapsed <= timeout);

                    if (_webElement is null)
                    {
                        throw lastException;
                    }
                }
                else
                {
                    throw new NotImplementedException($"Element locator context {From} is not supported yet.");
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
