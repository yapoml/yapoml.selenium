using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Factory;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public class BaseComponentList<TComponent, TListConditions, TComponentConditions> : IReadOnlyList<TComponent>
        where TComponent : BaseComponent
        where TListConditions : BaseComponentListConditions<TListConditions, TComponentConditions>
        where TComponentConditions : BaseComponentConditions<TComponentConditions>
    {
        protected TListConditions listConditions;

        private IList<TComponent> _list;

        private readonly BasePage _page;
        private readonly BaseComponent _parentComponent;
        private readonly IWebDriver _webDriver;
        private readonly IElementsListHandler _elementsListHandler;
        private readonly ComponentsListMetadata _componentsListMetadata;
        private readonly IEventSource _eventSource;
        private readonly ISpaceOptions _spaceOptions;

        public BaseComponentList(BasePage page, BaseComponent parentComponent, IWebDriver webDriver, IElementsListHandler elementsListHandler, ComponentsListMetadata componentsListMetadata, IEventSource eventSource, ISpaceOptions spaceOptions)
        {
            _page = page;
            _parentComponent = parentComponent;
            _webDriver = webDriver;
            _elementsListHandler = elementsListHandler;
            _componentsListMetadata = componentsListMetadata;
            _eventSource = eventSource;
            _spaceOptions = spaceOptions;
        }

        public TComponent this[int index]
        {
            get
            {
                EnsureLocated();

                if (index >= _list.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"Couldn't get a {_componentsListMetadata.ComponentMetadata.Name} by index {index} from {_list.Count} {_componentsListMetadata.Name}.");
                }

                return _list[index];
            }
        }

        public TComponent this[string text]
        {
            get
            {
                EnsureLocated();

                var component = _list.FirstOrDefault(c => c.Text == text);

                if (component is null)
                {
                    throw new InvalidOperationException($"{_componentsListMetadata.Name} contain no matching {_componentsListMetadata.ComponentMetadata.Name} with '{text}' text");
                }

                return component;
            }
        }

#if NET6_0_OR_GREATER
        public TComponent this[Func<TComponent, bool> predicate, [CallerArgumentExpression(nameof(predicate))] string predicateExpression = null]
#else
        public TComponent this[Func<TComponent, bool> predicate]
#endif
        {
            get
            {
                EnsureLocated();

                var component = _list.FirstOrDefault(predicate);

                if (component is null)
                {
#if NET6_0_OR_GREATER
                    throw new InvalidOperationException($"{_componentsListMetadata.Name} contain no matching {_componentsListMetadata.ComponentMetadata.Name} satisfying condition '{predicateExpression}'.");
#else
                    throw new InvalidOperationException($"{_componentsListMetadata.Name} contain no matching {_componentsListMetadata.ComponentMetadata.Name} satisfying condition.");
#endif

                }

                return component;
            }
        }

        public int Count
        {
            get
            {
                EnsureLocated();

                return _list.Count;
            }
        }

        public IEnumerator<TComponent> GetEnumerator()
        {
            EnsureLocated();

            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Performs the specified action on each component.
        /// </summary>
        /// <param name="action">The action to be performed.</param>
        public void ForEach(Action<TComponent> action)
        {
            EnsureLocated();

            foreach (var item in _list)
            {
                action(item);
            }
        }

        /// <summary>
        /// Various awaitable conditions on the list of components.
        /// </summary>
        public BaseComponentList<TComponent, TListConditions, TComponentConditions> Expect(Action<TListConditions> it)
        {
            it(listConditions);

            return this;
        }

        private void EnsureLocated()
        {
            if (_list == null)
            {
                var factory = _spaceOptions.Services.Get<IComponentFactory>();
                var locator = _spaceOptions.Services.Get<IElementLocator>();

                var elements = _elementsListHandler.LocateMany();

                _list = new List<TComponent>(elements.Select(e => factory.Create<TComponent, TListConditions>(_page, _parentComponent, _webDriver, new ElementHandler(_webDriver, null, locator, _elementsListHandler.By, e, _componentsListMetadata.ComponentMetadata, _elementsListHandler.ElementHandlerRepository.CreateNestedRepository(), _eventSource), _componentsListMetadata.ComponentMetadata, _spaceOptions)));
            }
        }
    }
}
