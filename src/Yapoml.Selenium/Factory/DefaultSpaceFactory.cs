using OpenQA.Selenium;
using System;

namespace Yapoml.Selenium.Factory
{
    public class DefaultSpaceFactory : ISpaceFactory
    {
        public TSpace Create<TSpace>(IWebDriver webDriver, Options.ISpaceOptions spaceOptions)
        {
            var space = (TSpace)Activator.CreateInstance(typeof(TSpace), webDriver, spaceOptions);

            return space;
        }
    }
}
