namespace Yapoml.Selenium.Services.Locator
{
    public interface IElementHandlerRepository
    {
        bool TryGet(string key, out IElementHandler elementHandler);

        void Set(string key, IElementHandler elementHandler);
    }
}
