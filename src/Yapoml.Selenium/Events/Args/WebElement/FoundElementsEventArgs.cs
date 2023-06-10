using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FoundElementsEventArgs : EventArgs
    {
        public FoundElementsEventArgs(By by, IWebDriver webDriver, IReadOnlyList<IWebElement> elements, ComponentsListMetadata componentsListMetadata)
        {
            By = by;
            WebDriver = webDriver;
            Elements = elements;
            ComponentsListMetadata = componentsListMetadata;
        }

        public By By { get; }
        public IWebDriver WebDriver { get; }
        public IReadOnlyList<IWebElement> Elements { get; }
        public ComponentsListMetadata ComponentsListMetadata { get; }
    }
}
