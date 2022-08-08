using OpenQA.Selenium;
using Yapoml.Framework.Options;

namespace Yapoml.Selenium.Services.Factory
{
    public interface ISpaceFactory
    {
        TSpace Create<TSpace>(IWebDriver webDriver, ISpaceOptions spaceOptions);
    }
}
