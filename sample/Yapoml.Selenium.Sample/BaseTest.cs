using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Yapoml.Selenium.Sample
{
    internal abstract class BaseTest
    {
        protected IWebDriver _webDriver;

        [SetUp]
        public void BaseSetUp()
        {
            var options = new ChromeOptions();

            options.AddArguments("--headless=new");

            _webDriver = new ChromeDriver(options);
        }

        [TearDown]
        public void BaseTearDown()
        {
            _webDriver?.Dispose();
        }
    }
}
