using System;

namespace Yapoml.Selenium.Components
{
    public abstract class BasePageConditions<TConditions> where TConditions: BasePageConditions<TConditions>
    {
        protected TConditions obj;

        public BasePageConditions(TimeSpan timeout, TimeSpan pollingInterval)
        {

        }

        public TConditions IsLoaded()
        {
            throw new NotImplementedException();

            return obj;
        }
    }
}
