using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FoundElementsEventArgs : EventArgs
    {
        public FoundElementsEventArgs(By by, IReadOnlyList<IWebElement> elements, ComponentsListMetadata componentsListMetadata)
        {
            By = by;
            Elements = elements;
            ComponentsListMetadata = componentsListMetadata;
        }

        public By By { get; }

        public IReadOnlyList<IWebElement> Elements { get; }


        public ComponentsListMetadata ComponentsListMetadata { get; }
    }
}
