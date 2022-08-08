using OpenQA.Selenium;
using System;
using Yapoml.Framework.Options;

namespace Yapoml.Selenium.Services.Factory
{
    public class DefaultSpaceFactory : ISpaceFactory
    {
        public TSpace Create<TSpace>(IWebDriver webDriver, ISpaceOptions spaceOptions)
        {
            var space = (TSpace)Activator.CreateInstance(typeof(TSpace), webDriver, spaceOptions);

            return space;
        }
    }
}
