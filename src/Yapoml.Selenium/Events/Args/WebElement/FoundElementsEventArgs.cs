using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FoundElementsEventArgs : EventArgs
    {
        public FoundElementsEventArgs(By by, IReadOnlyList<IWebElement> elements)
        {
            By = by;
            Elements = elements;
        }

        public By By { get; }
        public IReadOnlyList<IWebElement> Elements { get; }
    }
}
