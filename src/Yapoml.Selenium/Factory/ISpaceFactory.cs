using OpenQA.Selenium;

namespace Yapoml.Selenium.Factory
{
    public interface ISpaceFactory
    {
        TSpace Create<TSpace>(IWebDriver webDriver, Options.ISpaceOptions spaceOptions);
    }
}
