using System;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class AttributesCollectionConditions<TConditions> : Conditions<TConditions>
    {
        private readonly IElementHandler _elementHandler;

        public AttributesCollectionConditions(TConditions conditions, IElementHandler elementHandler, TimeSpan timeout, TimeSpan pollingInterval, ILogger logger)
            : base(conditions, timeout, pollingInterval, logger)
        {
            _elementHandler = elementHandler;
        }

        public StringAttributeConditions<TConditions> this[string attributeName]
        {
            get
            {
                return new StringAttributeConditions<TConditions>(_conditions, _elementHandler, attributeName, _timeout, _pollingInterval, $"{attributeName} attribute of the {_elementHandler.ComponentMetadata.Name}", _logger);
            }
        }

        public StringAttributeConditions<TConditions> Href => this["href"];

        public StringAttributeConditions<TConditions> Value => this["value"];

        public StringAttributeConditions<TConditions> Class => this["class"];

        public StringAttributeConditions<TConditions> Style => this["style"];

        public NumericAttributeConditions<TConditions, int> Width =>
            new NumericAttributeConditions<TConditions, int>(_conditions, _elementHandler, "width", _timeout, _pollingInterval, $"width attribute of the {_elementHandler.ComponentMetadata.Name}", _logger);

        public NumericAttributeConditions<TConditions, int> TabIndex =>
            new NumericAttributeConditions<TConditions, int>(_conditions, _elementHandler, "tabindex", _timeout, _pollingInterval, $"tabindex attribute of the {_elementHandler.ComponentMetadata.Name}", _logger);
    }
}
