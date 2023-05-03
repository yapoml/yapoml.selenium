﻿using OpenQA.Selenium;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Services.Locator
{
    public interface IElementHandler
    {
        IWebElement Locate();

        void Invalidate();

        By By { get; }

        ComponentMetadata ComponentMetadata { get; }

        IElementHandlerRepository ElementHandlerRepository { get; }
    }
}
