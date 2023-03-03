using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FoundElementsEventArgs : EventArgs
    {
        public FoundElementsEventArgs(By by, IReadOnlyList<IWebElement> elements, ComponentMetadata componentMetadata)
        {
            By = by;
            Elements = elements;
            ComponentMetadata = componentMetadata;
        }

        public By By { get; }

        public IReadOnlyList<IWebElement> Elements { get; }


        public ComponentMetadata ComponentMetadata { get; }
    }
}
