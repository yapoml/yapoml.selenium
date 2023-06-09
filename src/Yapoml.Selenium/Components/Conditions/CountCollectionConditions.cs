using System;
using Yapoml.Selenium.Components.Conditions.Generic;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class CountCollectionConditions<TListConditions> : NumericConditions<TListConditions, int>
    {
        private readonly IElementsListHandler _elementsListHandler;

        public CountCollectionConditions(TListConditions listConditions, IElementsListHandler elementsListHandler, TimeSpan timeout, TimeSpan pollingInterval)
            : base(listConditions, timeout, pollingInterval)
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
    }
}