using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events.Args.WebElement;

namespace Yapoml.Selenium.Events
{
    public class ComponentEventSource : IComponentEventSource
    {
        public event EventHandler<FindingElementEventArgs> OnFindingComponent;
        public event EventHandler<FindingElementsEventArgs> OnFindingComponents;
        public event EventHandler<FoundElementsEventArgs> OnFoundComponents;
        public event EventHandler<FoundElementEventArgs> OnFoundComponent;

        public void RaiseOnFindingComponent(By by, ComponentMetadata componentMetadata)
        {
            OnFindingComponent?.Invoke(this, new FindingElementEventArgs(by, componentMetadata));
        }

        public void RaiseOnFindingComponents(By by, ComponentsListMetadata componentsListMetadata)
        {
            OnFindingComponents?.Invoke(this, new FindingElementsEventArgs(by, componentsListMetadata));
        }

        public void RaiseOnFoundComponent(By by, IWebDriver webDriver, IWebElement webElement, ComponentMetadata componentMetadata)
        {
            OnFoundComponent?.Invoke(this, new FoundElementEventArgs(by, webDriver, webElement, componentMetadata));
        }

        public void RaiseOnFoundComponents(By by, IWebDriver webDriver, IReadOnlyList<IWebElement> elements, ComponentsListMetadata componentsListMetadata)
        {
            OnFoundComponents?.Invoke(this, new FoundElementsEventArgs(by, webDriver, elements, componentsListMetadata));
        }
    }
}
