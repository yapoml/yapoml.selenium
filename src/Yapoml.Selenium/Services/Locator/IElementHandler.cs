using OpenQA.Selenium;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Services.Locator
{
    public interface IElementHandler
    {
        IWebElement Locate();

        void Invalidate();

        IReadOnlyList<IWebElement> LocateMany();

        By By { get; }

        ComponentMetadata ComponentMetadata { get; }
    }
}
