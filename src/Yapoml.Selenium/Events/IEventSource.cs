namespace Yapoml.Selenium.Events
{
    public interface IEventSource
    {
        IPageEventSource PageEventSource { get; }

        IComponentEventSource ComponentEventSource { get; }
    }
}
