using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Services.Locator
{
    public interface IElementHandler
    {
        IWebElement Locate();

        IWebElement Locate(TimeSpan timeout, TimeSpan pollingInterval);

        void Invalidate();

        By By { get; }

        ElementLocatorContext From { get; }

        ComponentMetadata ComponentMetadata { get; }

        IElementHandlerRepository ElementHandlerRepository { get; }
    }
}
