using OpenQA.Selenium;
using System.Collections.Generic;

namespace Yapoml.Selenium.Services.Locator
{
    public interface IElementHandler
    {
        IWebElement Locate();

        IReadOnlyList<IWebElement> LocateMany();

        By By { get; }
    }
}
