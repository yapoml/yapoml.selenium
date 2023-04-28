using System;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events.Args.Page;

namespace Yapoml.Selenium.Events
{
    public interface IPageEventSource
    {
        event EventHandler<PageNavigatingEventArgs> OnPageNavigating;

        void RaiseOnPageNavigating(BasePage page, Uri uri, PageMetadata metadata);
    }
}
