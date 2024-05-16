using OpenQA.Selenium;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Services.Locator
{
    public interface IElementsListHandler
    {
        void Invalidate();

        IReadOnlyList<IWebElement> LocateMany();

        By By { get; }

        ElementLocatorContext From { get; }

        ComponentsListMetadata ComponentsListMetadata { get; }

        IElementHandlerRepository ElementHandlerRepository { get; }
    }
}
