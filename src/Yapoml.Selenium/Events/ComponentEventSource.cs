using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yapoml.Selenium.Events.Args.WebElement;

namespace Yapoml.Selenium.Events
{
    public class ComponentEventSource : IComponentEventSource
    {
        public event EventHandler<FindingElementEventArgs> OnFindingComponent;
        public event EventHandler<FindingElementEventArgs> OnFindingComponents;
        public event EventHandler<FoundElementsEventArgs> OnFoundComponents;
        public event EventHandler<FoundElementEventArgs> OnFoundComponent;

        public void RaiseOnFindingComponent(string componentName, By by)
        {
            OnFindingComponent?.Invoke(this, new FindingElementEventArgs(componentName, by));
        }

        public void RaiseOnFindingComponents(string componentName, By by)
        {
            OnFindingComponents?.Invoke(this, new FindingElementEventArgs(componentName, by));
        }

        public void RaiseOnFoundComponent(By by, IWebDriver webDriver, IWebElement webElement)
        {
            OnFoundComponent?.Invoke(this, new FoundElementEventArgs(by, webDriver, webElement));
        }

        public void RaiseOnFoundComponents(By by, IReadOnlyList<IWebElement> elements)
        {
            OnFoundComponents?.Invoke(this, new FoundElementsEventArgs(by, elements));
        }
    }
}
