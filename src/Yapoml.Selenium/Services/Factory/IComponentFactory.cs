using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Services.Factory
{
    public interface IComponentFactory
    {
        TComponent Create<TComponent>(IWebDriver webDriver, IWebElement webElement, ISpaceOptions spaceOptions) where TComponent : BaseComponent;
    }
}
