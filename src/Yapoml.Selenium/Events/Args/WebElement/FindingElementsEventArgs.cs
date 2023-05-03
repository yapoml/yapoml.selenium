using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FindingElementsEventArgs : EventArgs
    {
        public FindingElementsEventArgs(By by, ComponentsListMetadata componentsLIstMetadata)
        {
            By = by;
            ComponentsListMetadata = componentsLIstMetadata;
        }

        public By By { get; }

        public ComponentsListMetadata ComponentsListMetadata { get; }
    }
}
