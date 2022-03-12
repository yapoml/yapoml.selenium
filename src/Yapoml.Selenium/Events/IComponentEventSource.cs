using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Yapoml.Selenium.Events.Args.WebElement;

namespace Yapoml.Selenium.Events
{
    public interface IComponentEventSource
    {
        event EventHandler<FindingElementEventArgs> OnFindingComponent;

        event EventHandler<FoundElementEventArgs> OnFoundComponent;

        event EventHandler<FindingElementEventArgs> OnFindingComponents;

        event EventHandler<FoundElementsEventArgs> OnFoundComponents;

        void RaiseOnFindingComponent(string componentName, By by);

        void RaiseOnFindingComponents(string componentName, By by);

        void RaiseOnFoundComponents(By by, IReadOnlyList<IWebElement> elements);

        void RaiseOnFoundComponent(By by, IWebDriver webDriver, IWebElement webElement);
    }
}
