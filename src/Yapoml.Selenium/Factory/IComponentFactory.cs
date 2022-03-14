using OpenQA.Selenium;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Factory
{
    public interface IComponentFactory
    {
        TComponent Create<TComponent>(IWebDriver webDriver, IWebElement webElement, Options.ISpaceOptions spaceOptions) where TComponent : BaseComponent;
    }
}
