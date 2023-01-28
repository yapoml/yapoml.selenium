using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Services.Factory
{
    public interface IComponentFactory
    {
        TComponent Create<TComponent>(IWebDriver webDriver, IElementHandler elementHandler, ComponentMetadata componentMetada, ISpaceOptions spaceOptions) where TComponent : BaseComponent<TComponent>;
    }
}
