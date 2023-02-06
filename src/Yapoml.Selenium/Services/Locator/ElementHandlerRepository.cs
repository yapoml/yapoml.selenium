using System.Collections.Generic;

namespace Yapoml.Selenium.Services.Locator
{
    public class ElementHandlerRepository : IElementHandlerRepository
    {
        private readonly IDictionary<string, IElementHandler> _cache = new Dictionary<string, IElementHandler>();

        public ElementHandlerRepository()
        {

        }

        public ElementHandlerRepository(IElementHandlerRepository parentRepository)
        {
            ParentRepository = parentRepository;
        }

        public IElementHandlerRepository ParentRepository { get; private set; }

        public IElementHandlerRepository NestedRepository { get; private set; }

        public bool TryGet(string key, out IElementHandler elementHandler)
        {
            return _cache.TryGetValue(key, out elementHandler);
        }

        public void Set(string key, IElementHandler elementHandler)
        {
            _cache[key] = elementHandler;
        }

        public IElementHandlerRepository CreateNestedRepository()
        {
            if (NestedRepository is null)
            {
                NestedRepository = new ElementHandlerRepository(parentRepository: this);
            }

            return NestedRepository;
        }
    }
}
