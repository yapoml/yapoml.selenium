using OpenQA.Selenium;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Services.Factory
{
    public class DefaultSpaceFactory : ISpaceFactory
    {
        public TSpace Create<TSpace>(BaseSpace parentSpace, IWebDriver webDriver, ISpaceOptions spaceOptions)
        {
            var space = (TSpace)Activator.CreateInstance(typeof(TSpace), parentSpace, webDriver, spaceOptions);

            return space;
        }
    }
}
