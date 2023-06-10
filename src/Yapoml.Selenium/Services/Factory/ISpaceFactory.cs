using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Services.Factory
{
    public interface ISpaceFactory
    {
        TSpace Create<TSpace>(BaseSpace parentSpace, IWebDriver webDriver, ISpaceOptions spaceOptions);
    }
}
