using OpenQA.Selenium;
using System.Linq;
using System.Reflection;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Events;
using System.Collections.ObjectModel;
using System.Drawing;
using Yapoml.Selenium.Options;
using OpenQA.Selenium.Interactions;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Services.Locator;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Components
{
    /// <inheritdoc cref="IWebElement"/>
    public abstract class BaseComponent : IWebElement, IWrapsElement, ITakesScreenshot
    {
        private ILogger _logger;

        protected IWebDriver WebDriver { get; private set; }

        protected IElementHandler _elementHandler;

        public virtual IWebElement WrappedElement => _elementHandler.Locate();

        protected ComponentMetadata Metadata { get; }

        protected ISpaceOptions SpaceOptions { get; private set; }

        protected IEventSource EventSource { get; private set; }

        public BaseComponent(IWebDriver webDriver, IElementHandler elementHandler, ComponentMetadata metadata, ISpaceOptions spaceOptions)
        {
            WebDriver = webDriver;
            _elementHandler = elementHandler;
            Metadata = metadata;
            SpaceOptions = spaceOptions;

            EventSource = spaceOptions.Services.Get<IEventSource>();
            _logger = spaceOptions.Services.Get<ILogger>();
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
            _logger.Trace($"Clearing {Metadata.Name} component");

            WrappedElement.Clear();
        }

        /// <summary>
        /// Simulates a mouse click on an element.
        /// </summary>
        public virtual void Click()
        {
            _logger.Trace($"Clicking on {Metadata.Name} component");

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
            // todo make it event based
            if (this.GetType().Name.ToLowerInvariant().Contains("password") && text != null)
            {
                _logger.Trace($"Typing '{new string('*', text.Length)}' into {Metadata.Name} component");
            }
            else
            {
                _logger.Trace($"Typing '{text}' into {Metadata.Name} component");
            }

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
            _logger.Trace($"Hovering over {Metadata.Name} component");

            new Actions(WebDriver).MoveToElement(WrappedElement).Build().Perform();
        }

        /// <summary>
        /// Moves the cursor onto the element or one of its child elements.
        /// </summary>
        public virtual void Hover(int x, int y)
        {
            _logger.Trace($"Hovering on {Metadata.Name} component by X: {x}, Y: {y}");

            new Actions(WebDriver).MoveToElement(WrappedElement, x, y).Build().Perform();
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
                _logger.Trace($"Scrolling {Metadata.Name} component into view");

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

            _logger.Trace($"Scrolling {Metadata.Name} component into view with options {options}");

            var js = $"arguments[0].scrollIntoView({options.ToJson()});";

            (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement);
        }

        /// <summary>
        /// Sets focus on the specified element, if it can be focused.
        /// The focused element is the element that will receive keyboard and similar events by default.
        /// </summary>
        public virtual void Focus()
        {
            if (SpaceOptions.Services.TryGet<FocusOptions>(out var options))
            {
                Focus(options);
            }
            else
            {
                _logger.Trace($"Focusing {Metadata.Name} component");

                var js = "arguments[0].focus();";

                (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement);
            }
        }

        /// <summary>
        /// Sets focus on the specified element, if it can be focused.
        /// The focused element is the element that will receive keyboard and similar events by default.
        /// </summary>
        public virtual void Focus(FocusOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            _logger.Trace($"Focusing {Metadata.Name} component with options {options}");

            var js = $"arguments[0].focus({options.ToJson()});";

            (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement);
        }

        /// <summary>
        /// Removes keyboard focus from the element.
        /// </summary>
        public virtual void Blur()
        {
            _logger.Trace($"Bluring {Metadata.Name} component");

            var js = $"arguments[0].blur();";

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