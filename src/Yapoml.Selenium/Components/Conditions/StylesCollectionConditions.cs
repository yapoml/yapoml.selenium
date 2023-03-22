using System;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class StylesCollectionConditions<TConditions>
    {
        private readonly TConditions _conditions;
        private readonly IElementHandler _elementHandler;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval;

        public StylesCollectionConditions(TConditions conditions, IElementHandler elementHandler, TimeSpan timeout, TimeSpan pollingInterval)
        {
            _conditions = conditions;
            _elementHandler = elementHandler;
            _timeout = timeout;
            _pollingInterval = pollingInterval;
        }

        public StringStyleConditions<TConditions> this[string styleName]
        {
            get
            {
                return new StringStyleConditions<TConditions>(_conditions, _elementHandler, styleName, _timeout, _pollingInterval);
            }
        }

        // TODO make it strongly typed
        public StringStyleConditions<TConditions> Color => this["color"];

        public NumericStyleConditions<TConditions, double> Opacity =>
            new NumericStyleConditions<TConditions, double>(_conditions, _elementHandler, "opacity", _timeout, _pollingInterval);
    }
}
