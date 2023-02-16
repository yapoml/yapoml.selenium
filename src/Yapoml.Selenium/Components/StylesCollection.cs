using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public class StylesCollection
    {
        private readonly IElementHandler _elementHandler;

        public StylesCollection(IElementHandler elementHandler)
        {
            _elementHandler = elementHandler;
        }

        public string this[string name]
        {
            get
            {
                return RelocateOnStaleReference(() => _elementHandler.Locate().GetCssValue(name));
            }
        }

        public string Color => this["color"];

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

                _elementHandler.Locate();

                return act();
            }
        }
    }
}
