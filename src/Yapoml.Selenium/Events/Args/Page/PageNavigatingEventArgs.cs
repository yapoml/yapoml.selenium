using System;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Components.Metadata;

namespace Yapoml.Selenium.Events.Args.Page
{
    public class PageNavigatingEventArgs : EventArgs
    {
        public PageNavigatingEventArgs(BasePage page, Uri uri, PageMetadata metadata)
        {
            Page = page;
            Uri = uri;
            Metadata = metadata;
        }

        public BasePage Page { get; }

        public Uri Uri { get; }

        public PageMetadata Metadata { get; }
    }
}
