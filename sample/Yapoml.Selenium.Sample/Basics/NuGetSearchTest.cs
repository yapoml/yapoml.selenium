using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Yapoml.Selenium.Sample.Basics
{
    [TestFixture]
    public class NuGetSearchTest
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void SetUp()
        {
            _webDriver = new ChromeDriver();

            _webDriver.Navigate().GoToUrl("https://nuget.org");
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver?.Quit();
        }

        [Test]
        public void SearchWithSelenium()
        {
            _webDriver.FindElement(By.Id("search")).SendKeys("yaml");
            _webDriver.FindElement(By.CssSelector(".btn-search")).Click();

            foreach (var package in _webDriver.FindElements(By.CssSelector(".package")))
            {
                Assert.That(package.FindElement(By.XPath(".//a")).Text, Is.Not.Empty);
                Assert.That(package.FindElement(By.CssSelector(".package-details")).Text, Is.Not.Empty);
            }
        }

        [Test]
        public void SearchWithYapoml()
        {
            var ya = _webDriver.Ya(
                //opts => opts.UseLighter(delay: 200, fadeOutSpeed: 400)
                ).Basics.Pages;

            // ya.HomePage.When(it => it.IsLoaded());
            // ya.HomePage.SearchButton.When(it => it.IsDisplayed()).Click();

            ya.HomePage.SearchButton.Click(when => when.IsEnabled());
            ya.HomePage.SearchButton.When(it => it.IsEnabled()).Click();

            var page = ya.HomePage;

            // page.When(it => it.A.B.C.IsDisplayed(TimeSpan.FromSeconds(5)));

            for (int i = 0; i < 5; i++)
            {
                //page.When(it => it.SearchButton.IsDisplayed()).SearchButton.Click();
                //_webDriver.Navigate().Refresh();
                Console.WriteLine(page.SearchButton.Displayed);
            }

            _webDriver.Navigate().Refresh();

            page.Search("yaml");

            foreach (var package in ya.PackagesPage.Packages)
            {
                package.ScrollIntoView();
                // Console.WriteLine(package.Title.Text);
                Assert.That(package.Title.Text, Is.Not.Empty);
                Assert.That(package.Description.Text, Is.Not.Empty);
            }

            var yamlPackage = ya.PackagesPage.GetPackage(name: "YamlDotNet");
            Console.WriteLine(yamlPackage.Description.Text);
        }

        [Test]
        public void NavigateWithYapoml()
        {
            var ya = _webDriver.Ya(opts =>
                opts.UseBaseUrl("https://nuget.org"))
                .Basics.Pages;

            // it opens https://nuget.org/packages?q=yaml
            Assert.That(ya.PackagesPage.Open(q: "yaml").Packages.Count, Is.EqualTo(20));

            // it opens https://nuget.org/packages/Newtonsoft.Json
            Console.WriteLine(ya.PackageDetailsPage.Open("Newtonsoft.Json").Version.Text);
        }

        [Test]
        public void WaitWithYapoml()
        {
            // set global timeout
            var homePage = _webDriver.Ya(opts =>
                    opts.WithTimeout(timeout: TimeSpan.FromSeconds(10)))
                .Basics.Pages.HomePage;

            // used global timeout
            var searchInput = homePage.SearchInput.When(it => it.IsDisplayed());

            // or explicitly only here
            var searchInput2 = homePage.SearchInput.When(it => it.IsDisplayed(timeout: TimeSpan.FromSeconds(20)));

            // or using awaitable components by default
            var searchInput3 = _webDriver.Ya(opts => opts.UseAwaitableComponents())
                .Basics.Pages.HomePage.SearchInput;
        }

        [Test]
        public void ScrollEachPackageIntoView()
        {
            var packagesPage = _webDriver.Ya(opts => opts.UseBaseUrl("https://nuget.org")).Basics.Pages.PackagesPage;

            foreach (var package in packagesPage.Open(q: "yaml").Packages)
            {
                package.ScrollIntoView();
            }
        }
    }
}