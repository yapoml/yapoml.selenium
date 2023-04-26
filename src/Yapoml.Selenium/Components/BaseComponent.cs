﻿using OpenQA.Selenium;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Events;
using System.Drawing;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Services.Locator;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Components
{
    /// <inheritdoc cref="IWebElement"/>
    public abstract partial class BaseComponent<TComponent, TConditions> : IWrapsElement, ITakesScreenshot where TComponent : BaseComponent<TComponent, TConditions>
    {
        protected TComponent component;
        protected TConditions conditions;

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

            _attributes = new Lazy<AttributesCollection>(() => new AttributesCollection(elementHandler));
            _styles = new Lazy<StylesCollection>(() => new StylesCollection(elementHandler));
        }

        public string Text => RelocateOnStaleReference(() => WrappedElement.Text);

        public bool Enabled => RelocateOnStaleReference(() => WrappedElement.Enabled);

        public bool Selected => RelocateOnStaleReference(() => WrappedElement.Selected);

        public Point Location => RelocateOnStaleReference(() => WrappedElement.Location);

        public Size Size => RelocateOnStaleReference(() => WrappedElement.Size);

        public bool Displayed
        {
            // TODO return bool for awaitable components (they throw )
            get
            {
                bool displayed;

                try
                {
                    displayed = RelocateOnStaleReference(() => WrappedElement.Displayed);
                }
                catch (Exception ex) when (ex is NoSuchElementException || ex is StaleElementReferenceException)
                {
                    displayed = false;
                }

                return displayed;
            }
        }

        private readonly Lazy<AttributesCollection> _attributes;

        public AttributesCollection Attributes => _attributes.Value;

        private readonly Lazy<StylesCollection> _styles;

        public StylesCollection Styles => _styles.Value;

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

        /// <summary>
        /// Various awaitable conditions on the component.
        /// </summary>
        public TComponent When(Action<TConditions> it)
        {
            it(conditions);

            return component;
        }

        /// <summary>
        /// Returns a text for the current component.
        /// </summary>
        /// <returns>Text of the currrent component.</returns>
        public override string ToString()
        {
            return Text;
        }

        private T RelocateOnStaleReference<T>(Func<T> act)
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

        private void RelocateOnStaleReference(Action act)
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