using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Services.Factory;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Sample.Responsive
{
    [TestFixture]
    public class NuGetNavigationTest
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();

            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            _webDriver.Navigate().GoToUrl("https://nuget.org");
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void NavigateToPackagesWithSelenium()
        {
            var menuItems = _webDriver.FindElement(By.Id("navbar")).FindElements(By.XPath("./ul[1]/li"));

            var packagesMenuItem = menuItems.First(mi => mi.FindElement(By.XPath("./a/span")).Text == "Packages");

            packagesMenuItem.Click();

            foreach (var package in _webDriver.FindElements(By.CssSelector(".package")))
            {
                Assert.That(package.FindElement(By.XPath(".//a")).Text, Is.Not.Empty);
                Assert.That(package.FindElement(By.CssSelector(".package-details")).Text, Is.Not.Empty);
            }
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void NavigateToPackagesWithYapoml(bool isMobile)
        {
            var ya = _webDriver.Ya(
                opts => opts.Services.Register<IPageFactory>(new MyPageObjectFactory(isMobile)))
                .Responsive.Pages;

            if (isMobile) _webDriver.Manage().Window.Size = new System.Drawing.Size(320, 575);

            // HomePage property returns instance of HomePage class or HomePage_1 class
            // depending on MyPageObjectFactory
            ya.HomePage.Navigate("Packages");

            foreach (var package in ya.PackagesPage.Packages)
            {
                Assert.That(package.Title.Text, Is.Not.Empty);
                Assert.That(package.Description.Text, Is.Not.Empty);
            }
        }

        class MyPageObjectFactory : IPageFactory
        {
            public MyPageObjectFactory(bool isMobile)
            {
                IsMobile = isMobile;
            }

            public bool IsMobile { get; }

            public TPage Create<TPage>(IWebDriver webDriver, IElementHandlerRepository elementHandlerRepository, ISpaceOptions spaceOptions) where TPage : Components.BasePage
            {
                Type pageType = typeof(TPage);

                if (IsMobile)
                {
                    var mobilePageType = typeof(TPage).Assembly.GetTypes()
                        .FirstOrDefault(t => t.IsAssignableTo(typeof(TPage)) && t.Name.EndsWith("1Page"));

                    if (mobilePageType != null)
                    {
                        pageType = mobilePageType;
                    }
                }

                return (TPage)Activator.CreateInstance(pageType, webDriver, elementHandlerRepository, spaceOptions);
            }
        }

    }
}