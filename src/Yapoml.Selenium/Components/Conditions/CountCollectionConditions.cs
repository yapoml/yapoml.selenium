using System;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Components.Conditions
{
    public class CountCollectionConditions<TListConditions>
    {
        private readonly TListConditions _listConditions;
        private readonly IElementsListHandler _elementsListHandler;
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _pollingInterval;

        public CountCollectionConditions(TListConditions listConditions, IElementsListHandler elementsListHandler, TimeSpan timeout, TimeSpan pollingInterval)
        {
            _listConditions = listConditions;
            _elementsListHandler = elementsListHandler;
            _timeout = timeout;
            _pollingInterval = pollingInterval;
        }

        public TListConditions Is(uint value, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {
            var actualTimeout = timeout ?? _timeout;
            var actualPollingInterval = pollingInterval ?? _pollingInterval;

            int? latestCount = null;

            bool condition()
            {
                _elementsListHandler.Invalidate();

                latestCount = _elementsListHandler.LocateMany().Count;

                return latestCount == value;
            }

            try
            {
                Services.Waiter.Until(condition, actualTimeout, actualPollingInterval);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException($"The count of the {_elementsListHandler.ComponentsListMetadata.Name} is not '{value}' yet. The latest count is {latestCount}.", ex);
            }

            return _listConditions;
        }
    }
}