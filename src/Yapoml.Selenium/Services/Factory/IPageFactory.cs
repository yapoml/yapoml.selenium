using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Services.Factory
{
    public interface IPageFactory
    {
        TPage Create<TPage>(IWebDriver webDriver, IElementHandlerRepository elementHandlerRepository, ISpaceOptions spaceOptions) where TPage : BasePage;
    }
}
