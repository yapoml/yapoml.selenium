using System;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class AttributesCollectionConditions<TConditions>
    {
        private readonly TConditions _conditions;
        private readonly IElementHandler _elementHandler;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval;

        public AttributesCollectionConditions(TConditions conditions, IElementHandler elementHandler, TimeSpan timeout, TimeSpan pollingInterval)
        {
            _conditions = conditions;
            _elementHandler = elementHandler;
            _timeout = timeout;
            _pollingInterval = pollingInterval;
        }

        public StringAttributeConditions<TConditions> this[string attributeName]
        {
            get
            {
                return new StringAttributeConditions<TConditions>(_conditions, _elementHandler, attributeName, _timeout, _pollingInterval);
            }
        }

        public StringAttributeConditions<TConditions> Href => this["href"];

        public StringAttributeConditions<TConditions> Value => this["value"];

        public StringAttributeConditions<TConditions> Class => this["class"];

        public NumericAttributeConditions<TConditions, int> Width =>
            new NumericAttributeConditions<TConditions, int>(_conditions, _elementHandler, "width", _timeout, _pollingInterval);

        public NumericAttributeConditions<TConditions, int> TabIndex =>
            new NumericAttributeConditions<TConditions, int>(_conditions, _elementHandler, "tabindex", _timeout, _pollingInterval);
    }
}
