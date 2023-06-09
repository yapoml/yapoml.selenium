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

            _fetchFunc = () =>
            {
                _elementsListHandler.Invalidate();

                return _elementsListHandler.LocateMany().Count;
            };
        }

        protected override Exception GetIsError(int? latestValue, int expectedValue, Exception innerException)
        {
            return new TimeoutException($"The count of the {_elementsListHandler.ComponentsListMetadata.Name} is not '{expectedValue}' yet. The latest count is {latestValue}.", innerException);
        }

        protected override Exception GetIsNotError(int? latestValue, int expectedValue, Exception innerException)
        {
            return new TimeoutException($"The count of the {_elementsListHandler.ComponentsListMetadata.Name} is not '{expectedValue}' yet. The latest count is {latestValue}.", innerException);
        }
    }
}