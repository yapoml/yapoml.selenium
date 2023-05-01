using OpenQA.Selenium;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Services.Factory
{
    public class DefaultComponentFactory : IComponentFactory
    {
        public TComponent Create<TComponent, TConditions>(BasePage page, BaseComponent parentComponent, IWebDriver webDriver, IElementHandler elementHandler, ComponentMetadata componentMetadata, ISpaceOptions spaceOptions) where TComponent : BaseComponent<TComponent, TConditions>
        {
            var component = (TComponent)Activator.CreateInstance(typeof(TComponent), page, parentComponent, webDriver, elementHandler, componentMetadata, spaceOptions);

            return component;
        }
    }
}
