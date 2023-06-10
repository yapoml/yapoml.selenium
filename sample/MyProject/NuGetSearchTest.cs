using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using Yapoml.Selenium;

namespace MyProject
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
        public void SearchWithYapoml()
        {
            var ya = _webDriver.Ya(
                //opts => opts.UseLighter(delay: 200, fadeOutSpeed: 400)
                );
            var page = ya.HomePage;

            ya.HomePage.SearchButton.Expect(it => it.Styles.Opacity.Is(1.0, TimeSpan.FromSeconds(3)));

            Console.WriteLine(page.SearchInput.IsFocused);

            page.SearchInput.Type("yaml").And.SearchButton.Click();

            var resPage = ya.PackagesPage;

            resPage.Packages.Expect(its => its.Count.IsGreaterThan(10).Count.IsLessThan(30).Count.Is(20).All(each => each.Text.Contains("Yaml")));
        }
    }
}