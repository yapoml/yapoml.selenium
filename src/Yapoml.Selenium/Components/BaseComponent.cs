using OpenQA.Selenium;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Events;
using System.Drawing;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Services.Locator;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium.Components
{
    /// <inheritdoc cref="IWebElement"/>
    public abstract partial class BaseComponent<TComponent, TConditions> : BaseComponent, IWrapsElement, ITakesScreenshot
        where TComponent : BaseComponent<TComponent, TConditions>
        where TConditions : BaseComponentConditions<TConditions>
    {
        protected TComponent component;

        protected TConditions conditions;

        public BaseComponent(BasePage page, BaseComponent parentComponent, IWebDriver webDriver, IElementHandler elementHandler, ComponentMetadata metadata, ISpaceOptions spaceOptions)
            : base(page, parentComponent, webDriver, elementHandler, metadata, spaceOptions)
        {

        }

        /// <summary>
        /// Various awaitable conditions on the component.
        /// </summary>
        public TComponent Expect(Action<TConditions> it)
        {
            it(conditions);

            return component;
        }

        public static bool operator ==(BaseComponent<TComponent, TConditions> component, string value)
        {
            if (component is null)
            {
                return value == null;
            }

            return component.Text == value;
        }

        public static bool operator !=(BaseComponent<TComponent, TConditions> component, string value)
        {
            return !(component == value);
        }
    }

    public abstract class BaseComponent
    {
        protected BaseComponent parentComponent;
        protected BasePage Page { get; }
        protected IWebDriver WebDriver { get; private set; }

        protected IElementHandler _elementHandler;
        private readonly Lazy<AttributesCollection> _attributes;
        private readonly Lazy<StylesCollection> _styles;
        protected ILogger _logger;

        protected TimeSpan _locateTimeout;
        protected TimeSpan _locatePollingInterval;

        public virtual IWebElement WrappedElement => _elementHandler.Locate(_locateTimeout, _locatePollingInterval);

        protected ComponentMetadata Metadata { get; }

        protected ISpaceOptions SpaceOptions { get; private set; }

        protected IEventSource EventSource { get; private set; }

        public BaseComponent(BasePage page, BaseComponent parentComponent, IWebDriver webDriver, IElementHandler elementHandler, ComponentMetadata metadata, ISpaceOptions spaceOptions)
        {
            Page = page;
            this.parentComponent = parentComponent;
            WebDriver = webDriver;
            _elementHandler = elementHandler;
            Metadata = metadata;
            SpaceOptions = spaceOptions;

            EventSource = spaceOptions.Services.Get<IEventSource>();
            _logger = spaceOptions.Services.Get<ILogger>();
            _locateTimeout = spaceOptions.Services.Get<TimeoutOptions>().Timeout;
            _locatePollingInterval = spaceOptions.Services.Get<TimeoutOptions>().PollingInterval;

            _attributes = new Lazy<AttributesCollection>(() => new AttributesCollection(elementHandler));
            _styles = new Lazy<StylesCollection>(() => new StylesCollection(elementHandler));
        }

        public AttributesCollection Attributes => _attributes.Value;

        public StylesCollection Styles => _styles.Value;

        public string Text => RelocateOnStaleReference(() => WrappedElement.Text);

        public bool IsEnabled => RelocateOnStaleReference(() => WrappedElement.Enabled);

        public bool IsSelected => RelocateOnStaleReference(() => WrappedElement.Selected);

        public bool IsInView
        {
            get
            {
                var js = @"
const rect = arguments[0].getBoundingClientRect();
return (
    rect.top >= 0 &&
    rect.left >= 0 &&
    rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
    rect.right <= (window.innerWidth || document.documentElement.clientWidth)
);";
                    

                return (bool)(RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement)));
            }
        }

        public Point Location => RelocateOnStaleReference(() => WrappedElement.Location);

        public Size Size => RelocateOnStaleReference(() => WrappedElement.Size);

        public bool IsDisplayed
        {
            get
            {
                bool displayed;

                try
                {
                    displayed = _elementHandler.Locate().Displayed;
                }
                catch (Exception ex) when (ex is NoSuchElementException || ex is StaleElementReferenceException)
                {
                    displayed = false;
                }

                return displayed;
            }
        }

        public bool IsFocused
        {
            get
            {
                var activeElement = WebDriver.SwitchTo().ActiveElement();
                return WrappedElement.Equals(activeElement);
            }
        }

        public ISearchContext GetShadowRoot()
        {
            return RelocateOnStaleReference(() => WrappedElement.GetShadowRoot());
        }

        public void Submit()
        {
            RelocateOnStaleReference(() => WrappedElement.Submit());
        }

        public Screenshot GetScreenshot()
        {
            return RelocateOnStaleReference(() => ((ITakesScreenshot)WrappedElement).GetScreenshot());
        }

        public override bool Equals(object obj)
        {
            var str = obj as string;

            if (str != null)
            {
                return str.Equals(Text);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Returns a text for the current component.
        /// </summary>
        /// <returns>Text of the currrent component.</returns>
        public override string ToString()
        {
            return Text;
        }

        protected T RelocateOnStaleReference<T>(Func<T> act)
        {
            try
            {
                return act();
            }
            catch (StaleElementReferenceException)
            {
                _elementHandler.Invalidate();

                _elementHandler.Locate();

                return act();
            }
        }

        protected void RelocateOnStaleReference(Action act)
        {
            try
            {
                act();
            }
            catch (StaleElementReferenceException)
            {
                _elementHandler.Invalidate();

                _elementHandler.Locate();

                act();
            }
        }
    }
}