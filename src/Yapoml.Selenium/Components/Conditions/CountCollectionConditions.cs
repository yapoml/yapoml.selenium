using System;
using Yapoml.Framework.Logging;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class CountCollectionConditions<TListConditions> : NumericConditions<TListConditions, int>
    {
        private readonly IElementsListHandler _elementsListHandler;

        public CountCollectionConditions(TListConditions listConditions, IElementsListHandler elementsListHandler, TimeSpan timeout, TimeSpan pollingInterval, ILogger logger)
            : base(listConditions, timeout, pollingInterval, $"the count of {elementsListHandler.ComponentsListMetadata.Name}", logger)
        {
            _elementsListHandler = elementsListHandler;
        }

        protected override Func<int?> FetchValueFunc => () =>
        {
            _elementsListHandler.Invalidate();

            return _elementsListHandler.LocateMany().Count;
        };

        protected override string GetIsError(int? latestValue, int expectedValue)
        {
            return $"The count of the {_elementsListHandler.ComponentsListMetadata.Name} is not '{expectedValue}' yet. The latest count is {latestValue}.";
        }

        protected override string GetIsNotError(int? latestValue, int expectedValue)
        {
            return $"The count of the {_elementsListHandler.ComponentsListMetadata.Name} is not '{expectedValue}' yet. The latest count is {latestValue}.";
        }

        protected override string GetIsGreaterThanError(int? latestValue, int expectedValue)
        {
            return $"The count of the {_elementsListHandler.ComponentsListMetadata.Name} is not greater than '{expectedValue}' yet. The latest count is {latestValue}.";
        }

        protected override string AtLeast(int? latestValue, int expectedValue)
        {
            return $"The count of the {_elementsListHandler.ComponentsListMetadata.Name} is not equal to or greater than '{expectedValue}' yet. The latest count is {latestValue}.";
        }

        protected override string GetIsLessThanError(int? latestValue, int expectedValue)
        {
            return $"The count of the {_elementsListHandler.ComponentsListMetadata.Name} is not less than '{expectedValue}' yet. The latest count is {latestValue}.";
        }
    }
}