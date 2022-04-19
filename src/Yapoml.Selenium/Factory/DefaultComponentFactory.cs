using OpenQA.Selenium;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Factory
{
    public class DefaultComponentFactory : IComponentFactory
    {
        public TComponent Create<TComponent>(IWebDriver webDriver, IWebElement webElement, ISpaceOptions spaceOptions) where TComponent : BaseComponent
        {
            var component = (TComponent)Activator.CreateInstance(typeof(TComponent), webDriver, webElement, spaceOptions);

            return component;
        }
    }
}
