using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium.Components
{
    partial class BaseComponent<TComponent, TConditions>
    {
        /// <summary>
        /// Clears the content of the element.
        /// </summary>
        public virtual TComponent Clear()
        {
            _logger.Trace($"Clearing {Metadata.Name} component");

            RelocateOnStaleReference(() => WrappedElement.Clear());

            return component;
        }

        /// <summary>
        /// Clears the content of the element.
        /// </summary>
        public virtual TComponent Clear(Action<TConditions> when)
        {
            when(conditions);

            return Clear();
        }

        /// <summary>
        /// Simulates a mouse click on an element.
        /// </summary>
        public virtual TComponent Click()
        {
            _logger.Trace($"Clicking on {Metadata.Name} component");

            RelocateOnStaleReference(() => WrappedElement.Click());

            return component;
        }

        /// <summary>
        /// Simulates a mouse click on an element.
        /// </summary>
        public virtual TComponent Click(Action<TConditions> when)
        {
            when(conditions);

            return Click();
        }

        /// <summary>
        /// Simulates typing text into the element.
        /// </summary>
        /// <param name="text">The text to type into the element.</param>
        public virtual TComponent SendKeys(string text)
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

            RelocateOnStaleReference(() => WrappedElement.SendKeys(text));

            return component;
        }

        /// <summary>
        /// Simulates typing text into the element.
        /// </summary>
        /// <param name="text">The text to type into the element.</param>
        public virtual TComponent SendKeys(string text, Action<TConditions> when)
        {
            when(conditions);

            return SendKeys(text);
        }

        /// <summary>
        /// Moves the cursor onto the element or one of its child elements.
        /// </summary>
        public virtual TComponent Hover()
        {
            _logger.Trace($"Hovering over {Metadata.Name} component");

            RelocateOnStaleReference(() => new Actions(WebDriver).MoveToElement(WrappedElement).Build().Perform());
            return component;
        }

        /// <summary>
        /// Moves the cursor onto the element or one of its child elements.
        /// </summary>
        public virtual TComponent Hover(Action<TConditions> when)
        {
            when(conditions);

            return Hover();
        }

        /// <summary>
        /// Moves the cursor onto the element or one of its child elements.
        /// </summary>
        public virtual TComponent Hover(int x, int y)
        {
            _logger.Trace($"Hovering on {Metadata.Name} component by X: {x}, Y: {y}");

            RelocateOnStaleReference(() => new Actions(WebDriver).MoveToElement(WrappedElement, x, y).Build().Perform());

            return component;
        }

        /// <summary>
        /// Moves the cursor onto the element or one of its child elements.
        /// </summary>
        public virtual TComponent Hover(int x, int y, Action<TConditions> when)
        {
            when(conditions);

            return Hover(x, y);
        }

        /// <summary>
        /// Scrolls the element's ancestor containers is visible to the user.
        /// </summary>
        public virtual TComponent ScrollIntoView()
        {
            if (SpaceOptions.Services.TryGet<ScrollIntoViewOptions>(out var options))
            {
                ScrollIntoView(options);
            }
            else
            {
                _logger.Trace($"Scrolling {Metadata.Name} component into view");

                var js = "arguments[0].scrollIntoView();";

                RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));
            }

            return component;
        }

        /// <summary>
        /// Scrolls the element's ancestor containers is visible to the user.
        /// </summary>
        public virtual TComponent ScrollIntoView(Action<TConditions> when)
        {
            if (SpaceOptions.Services.TryGet<ScrollIntoViewOptions>(out var options))
            {
                ScrollIntoView(options, when);
            }
            else
            {
                when(conditions);

                ScrollIntoView();
            }

            return component;
        }

        /// <summary>
        /// Scrolls the element's ancestor containers is visible to the user.
        /// </summary>
        public virtual TComponent ScrollIntoView(ScrollIntoViewOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            _logger.Trace($"Scrolling {Metadata.Name} component into view with options {options}");

            var js = $"arguments[0].scrollIntoView({options.ToJson()});";

            RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));

            return component;
        }

        /// <summary>
        /// Scrolls the element's ancestor containers is visible to the user.
        /// </summary>
        public virtual TComponent ScrollIntoView(ScrollIntoViewOptions options, Action<TConditions> when)
        {
            when(conditions);

            return ScrollIntoView(options);
        }

        /// <summary>
        /// Sets focus on the specified element, if it can be focused.
        /// The focused element is the element that will receive keyboard and similar events by default.
        /// </summary>
        public virtual TComponent Focus()
        {
            if (SpaceOptions.Services.TryGet<FocusOptions>(out var options))
            {
                Focus(options);
            }
            else
            {
                _logger.Trace($"Focusing {Metadata.Name} component");

                var js = "arguments[0].focus();";

                RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));
            }

            return component;
        }

        /// <summary>
        /// Sets focus on the specified element, if it can be focused.
        /// The focused element is the element that will receive keyboard and similar events by default.
        /// </summary>
        public virtual TComponent Focus(Action<TConditions> when)
        {
            if (SpaceOptions.Services.TryGet<FocusOptions>(out var options))
            {
                return Focus(options, when);
            }
            else
            {
                when(conditions);

                return Focus();
            }
        }

        /// <summary>
        /// Sets focus on the specified element, if it can be focused.
        /// The focused element is the element that will receive keyboard and similar events by default.
        /// </summary>
        public virtual TComponent Focus(FocusOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            _logger.Trace($"Focusing {Metadata.Name} component with options {options}");

            var js = $"arguments[0].focus({options.ToJson()});";

            RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));

            return component;
        }

        /// <summary>
        /// Sets focus on the specified element, if it can be focused.
        /// The focused element is the element that will receive keyboard and similar events by default.
        /// </summary>
        public virtual TComponent Focus(FocusOptions options, Action<TConditions> when)
        {
            when(conditions);

            return Focus(options);
        }

        /// <summary>
        /// Removes keyboard focus from the element.
        /// </summary>
        public virtual TComponent Blur()
        {
            _logger.Trace($"Bluring {Metadata.Name} component");

            var js = $"arguments[0].blur();";

            RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));

            return component;
        }

        /// <summary>
        /// Removes keyboard focus from the element.
        /// </summary>
        public virtual TComponent Blur(Action<TConditions> when)
        {
            when(conditions);

            return Blur();
        }
    }
}
