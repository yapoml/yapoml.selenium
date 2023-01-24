using System;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Events.Args.Page;

namespace Yapoml.Selenium.Events
{
    public class PageEventSource : IPageEventSource
    {
        public event EventHandler<PageNavigatingEventArgs> OnPageNavigating;

        public void RaiseOnPageNavigating(BasePage page, Uri uri)
        {
            OnPageNavigating?.Invoke(this, new PageNavigatingEventArgs(page, uri));
        }
    }
}
