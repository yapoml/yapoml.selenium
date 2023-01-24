using System.Collections.Generic;

namespace Yapoml.Selenium.Services.Locator
{
    public class ElementHandlerRepository : IElementHandlerRepository
    {
        private IDictionary<string, IElementHandler> _cache = new Dictionary<string, IElementHandler>();

        public bool TryGet(string key, out IElementHandler elementHandler)
        {
            return _cache.TryGetValue(key, out elementHandler);
        }

        public void Set(string key, IElementHandler elementHandler)
        {
            _cache[key] = elementHandler;
        }
    }
}
