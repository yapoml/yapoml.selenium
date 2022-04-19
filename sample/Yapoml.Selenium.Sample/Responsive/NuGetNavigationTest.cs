using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Linq;
using Yapoml.Selenium;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Sample.Responsive
{
    [TestFixture]
    public class NuGetNavigationTest
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void SetUp()
        {
            _webDriver = new FirefoxDriver();

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
            var myPageFactory = new MyPageObjectFactory(isMobile);

            var ya = _webDriver.Ya(opts => opts.Register<Factory.IPageFactory>(myPageFactory)).Responsive.Pages;

            if (isMobile) _webDriver.Manage().Window.Size = new System.Drawing.Size(320, 575);

            // HomePage property returns HomePage class or HomeMobilePage class depending on MyPageObjectFactory
            ya.HomePage.Navigate("Packages");

            foreach (var package in ya.PackagesPage.Packages)
            {
                Assert.That(package.Title.Text, Is.Not.Empty);
                Assert.That(package.Description.Text, Is.Not.Empty);
            }
        }

        class MyPageObjectFactory : Factory.IPageFactory
        {
            public MyPageObjectFactory(bool isMobile)
            {
                IsMobile = isMobile;
            }

            public bool IsMobile { get; }

            public TPage Create<TPage>(IWebDriver webDriver, Framework.Options.ISpaceOptions spaceOptions) where TPage : BasePage
            {
                Type pageType = typeof(TPage);

                if (IsMobile)
                {
                    var mobilePageType = typeof(TPage).Assembly.GetTypes()
                        .FirstOrDefault(t => t.IsAssignableTo(typeof(TPage)) && t.Name.Contains("Mobile"));

                    if (mobilePageType != null)
                    {
                        pageType = mobilePageType;
                    }
                }

                return (TPage)Activator.CreateInstance(pageType, webDriver, spaceOptions);
            }
        }

    }
}