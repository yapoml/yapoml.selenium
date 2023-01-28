using OpenQA.Selenium;
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
    public class BaseComponentList<T> : IReadOnlyList<T> where T : BaseComponent<T>
    {
        private IList<T> _list;

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

        public T this[int index]
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

        public IEnumerator<T> GetEnumerator()
        {
            EnsureLocated();

            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void EnsureLocated()
        {
            if (_list == null)
            {
                var factory = _spaceOptions.Services.Get<IComponentFactory>();
                var locator = _spaceOptions.Services.Get<IElementLocator>();

                var elements = _elementHandler.LocateMany();

                _list = new List<T>(elements.Select(e => factory.Create<T>(_webDriver, new ElementHandler(_webDriver, _elementHandler, locator, _elementHandler.By, e, _componentMetadata, _eventSource), _componentMetadata, _spaceOptions)));
            }
        }
    }
}
