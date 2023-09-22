using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium.Components
{
    partial class BaseComponent<TComponent, TConditions>
    {
        /// <summary>
        /// Clears the text from a component.
        /// <para>
        /// It is useful for deleting the existing text before entering new text.
        /// For example, you can use it to erase a query in a search box, or clear a password field.
        /// </para>
        /// </summary>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent Clear()
        {
            using (_logger.BeginLogScope($"Clearing {Metadata.Name}"))
            {
                RelocateOnStaleReference(() => WrappedElement.Clear());
            }

            return component;
        }

        /// <inheritdoc cref="Clear()"/>
        /// <param name="when">Condition to be satisfied before clearing a text.</param>
        public virtual TComponent Clear(Action<TConditions> when)
        {
            when(conditions);

            return Clear();
        }

        /// <summary>
        /// Sends a sequence of keystrokes to a component.
        /// <para>
        /// It is useful for entering text, selecting options, or performing keyboard shortcuts.
        /// For example, you can use it to type a query in a search box, choose a value from a dropdown menu, or press the enter key.
        /// </para>
        /// </summary>
        /// <param name="text">The text to be typed. Also supports <seealso cref="OpenQA.Selenium.Keys"/>.</param>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent Type(string text)
        {
            string scopeName;

            if (Metadata.Name.ToLowerInvariant().Contains("password") && text != null)
            {
                scopeName = $"Typing '{new string('*', text.Length)}' into {Metadata.Name}";
            }
            else
            {
                scopeName = $"Typing '{text}' into {Metadata.Name}";
            }

            using (_logger.BeginLogScope(scopeName))
            {
                RelocateOnStaleReference(() => WrappedElement.SendKeys(text));
            }

            return component;
        }

        /// <inheritdoc cref="Type(string)"/>
        /// <param name="when">Condition to be satisfied before simulating a mouse click.</param>
        public virtual TComponent Type(string text, Action<TConditions> when)
        {
            when(conditions);

            return Type(text);
        }

        /// <summary>
        /// Simulates a mouse click on a component. It can be used to interact with buttons, links,
        /// checkboxes, radio buttons, and other clickable components on a page.
        /// </summary>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent Click()
        {
            using (_logger.BeginLogScope($"Clicking on {Metadata.Name}"))
            {
                RelocateOnStaleReference(() => WrappedElement.Click());
            }

            return component;
        }

        /// <inheritdoc cref="Click()"/>/>
        /// <param name="when">Condition to be satisfied before simulating a mouse click.</param>
        public virtual TComponent Click(Action<TConditions> when)
        {
            when(conditions);

            return Click();
        }

        /// <inheritdoc cref="Click()"/>
        /// <param name="x">Coordinates offset by X-axis.</param>
        /// <param name="y">Coordinates offset by Y-axis.</param>
        public virtual TComponent Click(int x, int y)
        {
            using (_logger.BeginLogScope($"Clicking on {Metadata.Name} by X: {x}, Y: {y}"))
            {
                RelocateOnStaleReference(() => new Actions(WebDriver).MoveToElement(WrappedElement, x, y).Click().Build().Perform());
            }

            return component;
        }

        /// <inheritdoc cref="Click(int, int)"/>
        /// <inheritdoc cref="Click(Action{TConditions})"/>
        public virtual TComponent Click(int x, int y, Action<TConditions> when)
        {
            when(conditions);

            return Click(x, y);
        }

        /// <summary>
        /// Simulates a mouse hover over a component.
        /// <para>
        /// It is useful for triggering events that depend on the mouse cursor position, such as displaying tooltips, menus, or pop-ups.
        /// For example, you can use it to hover over a link to see its URL, or hover over a button to see its description.
        /// </para>
        /// </summary>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent Hover()
        {
            using (_logger.BeginLogScope($"Hovering over {Metadata.Name}"))
            {
                RelocateOnStaleReference(() => new Actions(WebDriver).MoveToElement(WrappedElement).Build().Perform());
            }

            return component;
        }

        /// <inheritdoc cref="Hover()"/>
        /// <param name="when">Condition to be satisfied before simulating a mouse click.</param>
        public virtual TComponent Hover(Action<TConditions> when)
        {
            when(conditions);

            return Hover();
        }

        /// <inheritdoc cref="Hover()"/>
        /// <param name="x">Coordinates offset by X-axis.</param>
        /// <param name="y">Coordinates offset by Y-axis.</param>
        public virtual TComponent Hover(int x, int y)
        {
            using (_logger.BeginLogScope($"Hovering on {Metadata.Name} by X: {x}, Y: {y}"))
            {
                RelocateOnStaleReference(() => new Actions(WebDriver).MoveToElement(WrappedElement, x, y).Build().Perform());
            }

            return component;
        }

        /// <inheritdoc cref="Hover(int, int)"/>
        /// <inheritdoc cref="Hover(Action{TConditions})"/>
        public virtual TComponent Hover(int x, int y, Action<TConditions> when)
        {
            when(conditions);

            return Hover(x, y);
        }

        /// <summary>
        /// Scrolls the web page until a component is visible.
        /// <para>
        /// It is useful for accessing components that are not in the current viewport, such as hidden or lazy-loaded components.
        /// For example, you can use it to scroll to the bottom of a page to see the footer, or scroll to a specific section of a page to see its content.
        /// </para>
        /// </summary>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent ScrollIntoView()
        {
            if (SpaceOptions.Services.TryGet<ScrollIntoViewOptions>(out var options))
            {
                ScrollIntoView(options);
            }
            else
            {
                using (_logger.BeginLogScope($"Scrolling {Metadata.Name} into view"))
                {
                    var js = "arguments[0].scrollIntoView();";

                    RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));
                }
            }

            return component;
        }

        /// <inheritdoc cref="ScrollIntoView()"/>
        /// <param name="when">Condition to be satisfied before scrolling a component into view.</param>
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

        /// <inheritdoc cref="ScrollIntoView()"/>
        /// <param name="options">Specified options how exactly scroll a component into view.</param>
        public virtual TComponent ScrollIntoView(ScrollIntoViewOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            using (_logger.BeginLogScope($"Scrolling {Metadata.Name} into view with options {options}"))
            {
                var js = $"arguments[0].scrollIntoView({options.ToJson()});";

                RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));
            }

            return component;
        }

        /// <inheritdoc cref="ScrollIntoView(ScrollIntoViewOptions)"/>
        /// <inheritdoc cref="ScrollIntoView(Action{TConditions})"/>
        public virtual TComponent ScrollIntoView(ScrollIntoViewOptions options, Action<TConditions> when)
        {
            when(conditions);

            return ScrollIntoView(options);
        }

        /// <summary>
        /// Sets the focus on a component.
        /// <para>
        /// It is useful for activating the component or preparing it for user input. For example,
        /// you can use it to focus on a text field before typing, or focus on a checkbox before clicking.
        /// </para>
        /// </summary>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent Focus()
        {
            if (SpaceOptions.Services.TryGet<FocusOptions>(out var options))
            {
                Focus(options);
            }
            else
            {
                using (_logger.BeginLogScope($"Focusing {Metadata.Name}"))
                {
                    var js = "arguments[0].focus();";

                    RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));
                }
            }

            return component;
        }

        /// <inheritdoc cref="Focus()"/>
        /// <param name="when">Condition to be satisfied before clearing a text.</param>
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

        /// <inheritdoc cref="Focus()"/>
        /// <param name="options">Specified aspects of the focusing process.</param>
        public virtual TComponent Focus(FocusOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            using (_logger.BeginLogScope($"Focusing {Metadata.Name} with options {options}"))
            {
                var js = $"arguments[0].focus({options.ToJson()});";

                RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));
            }

            return component;
        }

        /// <inheritdoc cref="Focus(FocusOptions)"/>
        /// <inheritdoc cref="Focus(Action{TConditions})"/>
        public virtual TComponent Focus(FocusOptions options, Action<TConditions> when)
        {
            when(conditions);

            return Focus(options);
        }

        /// <summary>
        /// Removes the focus from a component.
        /// <para>
        /// It is useful for deactivating the component or triggering events that depend on the focus state, such as validation or formatting.
        /// For example, you can use it to blur a text field after typing, or blur a dropdown menu after selecting an option.
        /// </para>
        /// </summary>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent Blur()
        {
            using (_logger.BeginLogScope($"Bluring {Metadata.Name}"))
            {
                var js = $"arguments[0].blur();";

                RelocateOnStaleReference(() => (WebDriver as IJavaScriptExecutor).ExecuteScript(js, WrappedElement));
            }

            return component;
        }

        /// <inheritdoc cref="Blur()"/>
        /// <param name="when">Condition to be satisfied before removing the focus.</param>
        public virtual TComponent Blur(Action<TConditions> when)
        {
            when(conditions);

            return Blur();
        }

        /// <summary>
        /// Simulates a mouse right click on a component. It can be used to interact with elements
        /// that show a context menu when right clicked, such as opening a link in a new tab, copying text.
        /// </summary>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent ContextClick()
        {
            using (_logger.BeginLogScope($"Context clicking on {Metadata.Name}"))
            {
                RelocateOnStaleReference(() => new Actions(WebDriver).ContextClick(WrappedElement).Build().Perform());
            }

            return component;
        }

        /// <inheritdoc cref="ContextClick()"/>
        /// <param name="when">Condition to be satisfied before simulating a mouse right click.</param>
        public virtual TComponent ContextClick(Action<TConditions> when)
        {
            when(conditions);

            return ContextClick();
        }

        /// <summary>
        /// Simulates a mouse double click on a component. It can be used to interact with elements
        /// that require a double click to launch specific functions, such as opening a file, selecting a word of text, etc.
        /// </summary>
        /// <returns>The same instance of the component to continue interaction with it.</returns>
        public virtual TComponent DoubleClick()
        {
            using (_logger.BeginLogScope($"Double clicking on {Metadata.Name}"))
            {
                RelocateOnStaleReference(() => new Actions(WebDriver).DoubleClick(WrappedElement).Build().Perform());
            }

            return component;
        }

        /// <inheritdoc cref="DoubleClick()"/>
        /// <param name="when">Condition to be satisfied before simulating a mouse double click.</param>
        public virtual TComponent DoubleClick(Action<TConditions> when)
        {
            when(conditions);

            return DoubleClick();
        }

        /// <summary>
        /// Performs a drag and drop operation to another component.
        /// </summary>
        public virtual TComponent DragAndDrop<TToComponent, TToConditions>(BaseComponent<TToComponent, TToConditions> toComponent)
            where TToComponent : BaseComponent<TToComponent, TToConditions>
            where TToConditions : BaseComponentConditions<TToConditions>
        {
            using (_logger.BeginLogScope($"Dragging {Metadata.Name} to {toComponent.Metadata.Name}"))
            {
                RelocateOnStaleReference(() => new Actions(WebDriver).DragAndDrop(WrappedElement, toComponent.WrappedElement).Build().Perform());
            }

            return component;
        }

        /// <summary>
        /// Performs a drag and drop operation to another component.
        /// </summary>
        public virtual TComponent DragAndDrop<TToComponent, TToConditions>(BaseComponent<TToComponent, TToConditions> toComponent, Action<TConditions> when)
            where TToComponent : BaseComponent<TToComponent, TToConditions>
            where TToConditions : BaseComponentConditions<TToConditions>
        {
            when(conditions);

            return DragAndDrop(toComponent);
        }
    }
}
