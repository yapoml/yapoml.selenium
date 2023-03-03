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
        public event EventHandler<FindingElementEventArgs> OnFindingComponents;
        public event EventHandler<FoundElementsEventArgs> OnFoundComponents;
        public event EventHandler<FoundElementEventArgs> OnFoundComponent;

        public void RaiseOnFindingComponent(By by, ComponentMetadata componentMetadata)
        {
            OnFindingComponent?.Invoke(this, new FindingElementEventArgs(by, componentMetadata));
        }

        public void RaiseOnFindingComponents(By by, ComponentMetadata componentMetadata)
        {
            OnFindingComponents?.Invoke(this, new FindingElementEventArgs(by, componentMetadata));
        }

        public void RaiseOnFoundComponent(By by, IWebDriver webDriver, IWebElement webElement, ComponentMetadata componentMetadata)
        {
            OnFoundComponent?.Invoke(this, new FoundElementEventArgs(by, webDriver, webElement, componentMetadata));
        }

        public void RaiseOnFoundComponents(By by, IReadOnlyList<IWebElement> elements, ComponentMetadata componentMetadata)
        {
            OnFoundComponents?.Invoke(this, new FoundElementsEventArgs(by, elements, componentMetadata));
        }
    }
}
