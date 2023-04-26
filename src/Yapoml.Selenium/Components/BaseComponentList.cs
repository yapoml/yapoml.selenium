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

        private readonly IWebDriver _webDriver;
        private readonly IElementHandler _elementHandler;
        private readonly ComponentMetadata _componentMetadata;
        private readonly IEventSource _eventSource;
        private readonly ISpaceOptions _spaceOptions;

        public BaseComponentList(IWebDriver webDriver, IElementHandler elementHandler, ComponentMetadata componentMetadata, IEventSource eventSource, ISpaceOptions spaceOptions)
        {
            _webDriver = webDriver;
            _elementHandler = elementHandler;
            _componentMetadata = componentMetadata;
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

                var elements = _elementHandler.LocateMany();

                _list = new List<TComponent>(elements.Select(e => factory.Create<TComponent, TConditions>(_webDriver, new ElementHandler(_webDriver, _elementHandler, locator, _elementHandler.By, e, _componentMetadata, _elementHandler.ElementHandlerRepository.CreateNestedRepository(), _eventSource), _componentMetadata, _spaceOptions)));
            }
        }
    }
}
