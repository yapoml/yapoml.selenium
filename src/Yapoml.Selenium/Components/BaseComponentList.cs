using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events;
using Yapoml.Selenium.Services.Factory;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public class BaseComponentList<TComponent, TConditions> : IReadOnlyList<TComponent> where TComponent : BaseComponent<TComponent, TConditions>
    {
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

                return _list[index];
            }
        }

#if NET6_0_OR_GREATER
        public TComponent this[Func<TComponent, bool> predicate, [System.Runtime.CompilerServices.CallerArgumentExpression("predicate")] string predicateExpression = null]
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
                    throw new InvalidOperationException($"{_componentsListMetadata.Name} contain no matching component satisfying condition {predicateExpression}");
#else
                    throw new InvalidOperationException($"{_componentsListMetadata.Name} contain no matching component.");
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

        private void EnsureLocated()
        {
            if (_list == null)
            {
                var factory = _spaceOptions.Services.Get<IComponentFactory>();
                var locator = _spaceOptions.Services.Get<IElementLocator>();

                var elements = _elementsListHandler.LocateMany();

                _list = new List<TComponent>(elements.Select(e => factory.Create<TComponent, TConditions>(_page, _parentComponent, _webDriver, new ElementHandler(_webDriver, null, locator, _elementsListHandler.By, e, _componentsListMetadata.ComponentMetadata, _elementsListHandler.ElementHandlerRepository.CreateNestedRepository(), _eventSource), _componentsListMetadata.ComponentMetadata, _spaceOptions)));
            }
        }
    }
}
