using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Drawing;
using Yapoml.Options;
using Yapoml.Selenium.Events;

namespace Yapoml.Selenium.Components
{
    /// <inheritdoc/>
    public partial class BaseComponent : IWebElement, IWrapsElement
    {
        protected IWebDriver WebDriver { get; private set; }

        public IWebElement WrappedElement { get; private set; }

        protected ISpaceOptions SpaceOptions { get; private set; }

        protected IComponentEventSource EventSource { get; private set; }

        public BaseComponent(IWebDriver webDriver, IWebElement webElement, ISpaceOptions spaceOptions)
        {
            WebDriver = webDriver;
            WrappedElement = webElement;
            SpaceOptions = spaceOptions;

            EventSource = spaceOptions.Get<IEventSource>().ComponentEventSource;
        }

        public string TagName => WrappedElement.TagName;

        public string Text => WrappedElement.Text;

        public bool Enabled => WrappedElement.Enabled;

        public bool Selected => WrappedElement.Selected;

        public Point Location => WrappedElement.Location;

        public Size Size => WrappedElement.Size;

        public bool Displayed => WrappedElement.Displayed;

        public void Clear()
        {
            WrappedElement.Clear();
        }

        public void Click()
        {
            WrappedElement.Click();
        }

        public IWebElement FindElement(By by)
        {
            return WrappedElement.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return WrappedElement.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return WrappedElement.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return WrappedElement.GetCssValue(propertyName);
        }

        public string GetDomAttribute(string attributeName)
        {
            return WrappedElement.GetDomAttribute(attributeName);
        }

        public string GetDomProperty(string propertyName)
        {
            return WrappedElement.GetDomProperty(propertyName);
        }

        [System.Obsolete]
        public string GetProperty(string propertyName)
        {
            return WrappedElement.GetProperty(propertyName);
        }

        public ISearchContext GetShadowRoot()
        {
            return WrappedElement.GetShadowRoot();
        }

        public void SendKeys(string text)
        {
            WrappedElement.SendKeys(text);
        }

        public void Submit()
        {
            WrappedElement.Submit();
        }
    }
}