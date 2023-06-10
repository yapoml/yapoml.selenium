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

        event EventHandler<FindingElementsEventArgs> OnFindingComponents;

        event EventHandler<FoundElementsEventArgs> OnFoundComponents;

        void RaiseOnFindingComponent(By by, ComponentMetadata componentMetadata);

        void RaiseOnFindingComponents(By by, ComponentsListMetadata componentsListMetadata);

        void RaiseOnFoundComponents(By by, IWebDriver webDriver, IReadOnlyList<IWebElement> elements, ComponentsListMetadata componentsListMetadata);

        void RaiseOnFoundComponent(By by, IWebDriver webDriver, IWebElement webElement, ComponentMetadata componentMetadata);
    }
}
