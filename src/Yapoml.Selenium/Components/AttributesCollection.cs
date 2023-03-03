using OpenQA.Selenium;
using System;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components
{
    public class AttributesCollection
    {
        private readonly IElementHandler _elementHandler;

        public AttributesCollection(IElementHandler elementHandler)
        {
            _elementHandler = elementHandler;
        }

        public string this[string name]
        {
            get
            {
                return RelocateOnStaleReference(() => _elementHandler.Locate().GetAttribute(name));
            }
        }

        public string Href => this["href"];

        public string Value => this["value"];

        public string Class => this["class"];

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