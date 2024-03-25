using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Yapoml.Selenium.Sample.Basics
{
    [TestFixture]
    class NuGetSearchTest : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            _webDriver.Navigate().GoToUrl("https://nuget.org");
        }

        [Test]
        public void SearchWithSelenium()
        {
            _webDriver.FindElement(By.Id("search")).SendKeys("selenium");
            _webDriver.FindElement(By.CssSelector(".btn-search")).Click();

            var packages = _webDriver.FindElements(By.CssSelector(".package"));

            Assert.That(packages.Count, Is.EqualTo(20));

            foreach (var package in packages)
            {
                Assert.That(package.FindElement(By.XPath(".//a")).Text, Is.Not.Empty);
                Assert.That(package.FindElement(By.CssSelector(".package-details")).Text, Is.Not.Empty);

                var tags = package.FindElements(By.CssSelector(".package-tags a"));

                foreach (var tag in tags)
                {
                    Assert.That(tag.Text, Is.Not.Empty);
                }
            }
        }

        [Test]
        public void SearchWithYapoml()
        {
            _webDriver.Ya().Basics.Pages
                .HomePage.Search("selenium")
                .Packages.Expect(its => its.Count.Is(20))
                .ForEach(package =>
                    {
                        package.Title.Expect(it => it.Text.IsNot(""));
                        package.Description.Expect(it => it.Text.IsNot(""));
                        package.Tags.ForEach(tag => tag.Expect(it => it.Text.IsNot("")));
                    });
        }

        [Test]
        public void NavigateWithYapoml()
        {
            var ya = _webDriver.Ya(opts =>
                opts.WithBaseUrl("https://nuget.org"))
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
            var searchInput = homePage.SearchInput.Expect(it => it.IsDisplayed());

            // or explicitly only here
            var searchInput2 = homePage.SearchInput.Expect(it => it.IsDisplayed(timeout: TimeSpan.FromSeconds(20)));
        }

        [Test]
        public void ScrollEachPackageIntoView()
        {
            var packagesPage = _webDriver.Ya(opts => opts.WithBaseUrl("https://nuget.org")).Basics.Pages.PackagesPage;

            foreach (var package in packagesPage.Open(q: "yaml").Packages)
            {
                package.ScrollIntoView();
            }
        }

        [Test]
        public void Cache()
        {
            var page = _webDriver.Ya().Basics.Pages.HomePage;
            page.Expect(it => it.Title.Matches(new System.Text.RegularExpressions.Regex("Home"), TimeSpan.FromSeconds(3)));

            page.Search("yapoml");

            var packagesPage = _webDriver.Ya().Basics.Pages.PackagesPage;

            foreach (var package in packagesPage.Packages)
            {
                Console.WriteLine(package.Title.Text);
            }

            var myPackage = packagesPage.Packages[p => p.Title == "Yapoml.Selenium"];
            Console.Write(myPackage);
        }

        [Test]
        public void CustomExpectation()
        {
            var page = _webDriver.Ya().Basics.Pages.HomePage.Expect(its => its.SearchButton.IsNotWhite());

            page.SearchButton.Click(when => when.IsNotWhite());
        }
    }
}