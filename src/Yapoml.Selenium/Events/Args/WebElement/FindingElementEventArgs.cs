using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FindingElementEventArgs : EventArgs
    {
        public FindingElementEventArgs(By by, ComponentMetadata componentMetadata)
        {
            By = by;
            ComponentMetadata = componentMetadata;
        }

        public By By { get; }

        public ComponentMetadata ComponentMetadata { get; }
    }
}
