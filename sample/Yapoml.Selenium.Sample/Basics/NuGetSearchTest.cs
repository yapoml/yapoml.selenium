using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
                opts => opts.UseLighter(delay: 200, fadeOutSpeed: 400)
                ).Basics.Pages;

            ya.HomePage.Search("yaml");

            foreach (var package in ya.PackagesPage.Packages)
            {
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
    }
}