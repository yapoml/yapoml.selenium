using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Services.Factory
{
    public interface IComponentFactory
    {
        TComponent Create<TComponent, TConditions>(BasePage page, BaseComponent parentComponent, IWebDriver webDriver, IElementHandler elementHandler, ComponentMetadata componentMetadata, ISpaceOptions spaceOptions) where TComponent : BaseComponent;
    }
}
