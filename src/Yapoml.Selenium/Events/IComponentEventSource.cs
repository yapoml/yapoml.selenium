using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events.Args.WebElement;

namespace Yapoml.Selenium.Events
{
    public interface IComponentEventSource
    {
        event EventHandler<FindingElementEventArgs> OnFindingComponent;

        event EventHandler<FoundElementEventArgs> OnFoundComponent;

        event EventHandler<FindingElementEventArgs> OnFindingComponents;

        event EventHandler<FoundElementsEventArgs> OnFoundComponents;

        void RaiseOnFindingComponent(By by, ComponentMetadata componentMetadata);

        void RaiseOnFindingComponents(By by, ComponentMetadata componentMetadata);

        void RaiseOnFoundComponents(By by, IReadOnlyList<IWebElement> elements, ComponentMetadata componentMetadata);

        void RaiseOnFoundComponent(By by, IWebDriver webDriver, IWebElement webElement, ComponentMetadata componentMetadata);
    }
}
