using OpenQA.Selenium;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Options;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public class StylesCollection
    {
        private readonly IElementHandler _elementHandler;

        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval;

        public StylesCollection(IElementHandler elementHandler, ISpaceOptions spaceOptions)
        {
            _elementHandler = elementHandler;

            _timeout = spaceOptions.Services.Get<TimeoutOptions>().Timeout;
            _pollingInterval = spaceOptions.Services.Get<TimeoutOptions>().PollingInterval;
        }

        public string this[string name]
        {
            get
            {
                return RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(name));
            }
        }

        public string Color => this["color"];

        public string BackgroundColor => this["background-color"];

        public string Opacity => this["opacity"];

        private T RelocateOnStaleReference<T>(Func<T> act)
        {
            try
            {
                return act();
            }
            catch (StaleElementReferenceException)
            {
                _elementHandler.Invalidate();

                _elementHandler.Locate(_timeout, _pollingInterval);

                return act();
            }
        }
    }
}
