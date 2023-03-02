using System.Collections.Generic;
using System.Linq;

namespace Yapoml.Selenium.Services.Locator
{
    public class ElementHandlerRepository : IElementHandlerRepository
    {
        private readonly IDictionary<string, IElementHandler> _elementHandlers = new Dictionary<string, IElementHandler>();

        public ElementHandlerRepository()
        {

        }

        public ElementHandlerRepository(IElementHandlerRepository parentRepository)
        {
            ParentRepository = parentRepository;
        }

        public IElementHandlerRepository ParentRepository { get; private set; }

        public bool TryGet(string key, out IElementHandler elementHandler)
        {
            return _elementHandlers.TryGetValue(key, out elementHandler);
        }

        public void Set(string key, IElementHandler elementHandler)
        {
            _elementHandlers[key] = elementHandler;
        }

        public IElementHandlerRepository CreateNestedRepository()
        {
            return new ElementHandlerRepository(parentRepository: this);
        }

        public IReadOnlyCollection<IElementHandler> ElementHandlers
        {
            get
            {
                return _elementHandlers.Values.ToList().AsReadOnly();
            }
        }
    }
}
