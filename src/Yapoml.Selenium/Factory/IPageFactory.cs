using OpenQA.Selenium;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Factory
{
    public interface IPageFactory
    {
        TPage Create<TPage>(IWebDriver webDriver, Options.ISpaceOptions spaceOptions) where TPage : BasePage;
    }
}
