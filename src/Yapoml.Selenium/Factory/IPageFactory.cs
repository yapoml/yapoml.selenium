using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Factory
{
    public interface IPageFactory
    {
        TPage Create<TPage>(IWebDriver webDriver, ISpaceOptions spaceOptions) where TPage : BasePage;
    }
}
