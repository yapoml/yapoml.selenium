using NUnit.Framework;
using OpenQA.Selenium;

namespace Yapoml.Selenium.Sample
{
    [TestFixture]
    class NuGetSearchTest : BaseTest
    {
        [Test]
        public void SearchWithSelenium()
        {
            _webDriver.Navigate().GoToUrl("https://nuget.org");

            _webDriver.FindElement(By.Id("search")).SendKeys("selenium");
            _webDriver.FindElement(By.CssSelector(".btn-search")).Click();

            var packages = _webDriver.FindElements(By.CssSelector(".package"));

            Assert.That(packages.Count, Is.EqualTo(20));

            foreach (var package in packages)
            {
                StringAssert.Contains("Selenium", package.FindElement(By.XPath(".//a")).Text);
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
            _webDriver.Ya(opts => opts.WithBaseUrl("https://nuget.org"))
                .HomePage.Open().Search("Selenium")
                .Packages.Expect(its => its.Count.Is(20).Each(package =>
                    {
                        package.Title.Contains("Selenium");
                        package.Description.IsNotEmpty();
                        package.Tags.Each(tag => tag.IsNotEmpty());
                    })
                );
        }

        [Test]
        public void IntroShowcase()
        {
            _webDriver.Ya().PackagesPage.Open(q: "yapoml")
                .Packages.Expect(it => it.IsNotEmpty().Each(package =>
                    {
                        package.Description.IsNotEmpty();
                        package.Tags.IsNotEmpty();
                    })
                );
        }
    }
}
