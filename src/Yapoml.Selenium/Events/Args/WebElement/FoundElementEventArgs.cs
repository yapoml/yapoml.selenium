using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FoundElementEventArgs : EventArgs
    {
        public FoundElementEventArgs(By by, IWebDriver webDriver, IWebElement webElement, ComponentMetadata componentMetadata)
        {
            By = by;
            WebDriver = webDriver;
            WebElement = webElement;
            ComponentMetadata = componentMetadata;
        }

        public By By { get; }
        public IWebDriver WebDriver { get; }
        public IWebElement WebElement { get; }
        public ComponentMetadata ComponentMetadata { get; }
    }
}
