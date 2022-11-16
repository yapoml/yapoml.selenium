using System;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Events.Args.Page
{
    public class PageNavigatingEventArgs : EventArgs
    {
        public PageNavigatingEventArgs(BasePage page, Uri uri)
        {
            Page = page;
            Uri = uri;
        }

        public BasePage Page { get; }
        public Uri Uri { get; }
    }
}
