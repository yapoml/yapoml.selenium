﻿using OpenQA.Selenium;
using System.Linq;
using System.Reflection;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Events;
using System.Collections.ObjectModel;
using System.Drawing;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium.Components
{
    /// <inheritdoc cref="IWebElement"/>
    public abstract class BaseComponent : IWebElement, IWrapsElement, ITakesScreenshot
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

            EventSource = spaceOptions.Services.Get<IEventSource>().ComponentEventSource;
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

        public Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)WrappedElement).GetScreenshot();
        }

        /// <summary>
        /// Moves the cursor onto the element or one of its child elements.
        /// </summary>
        public virtual void Hover()
        {
            new OpenQA.Selenium.Interactions.Actions(WebDriver).MoveToElement(WrappedElement).Build().Perform();
        }

        /// <summary>
        /// Moves the cursor onto the element or one of its child elements.
        /// </summary>
        public virtual void Hover(int x, int y)
        {
            new OpenQA.Selenium.Interactions.Actions(WebDriver).MoveToElement(WrappedElement, x, y).Build().Perform();
        }

        /// <summary>
        /// Scrolls the element's ancestor containers is visible to the user.
        /// </summary>
        public virtual void ScrollIntoView()
        {
            if (SpaceOptions.Services.TryGet<ScrollIntoViewOptions>(out var options))
            {
                ScrollIntoView(options);
            }
            else
            {
                var js = "arguments[0].scrollIntoView();";

                (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement);
            }
        }

        /// <summary>
        /// Scrolls the element's ancestor containers is visible to the user.
        /// </summary>
        public virtual void ScrollIntoView(ScrollIntoViewOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            var js = $"arguments[0].scrollIntoView({options});";

            (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement);
        }

        /// <summary>
        /// Determine whether component has private method with specific signature.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected bool HasImplementation<T>(string methodName, params object[] args) where T : BaseComponent
        {
            Type[] argTypes = args.Select(arg => arg.GetType()).ToArray();

            var method = typeof(T).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance, null, argTypes, null);

            return method != null;
        }
    }
}