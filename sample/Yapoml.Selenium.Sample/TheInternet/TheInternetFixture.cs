using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Yapoml.Selenium.Sample.TheInternet.Pages;

namespace Yapoml.Selenium.Sample.TheInternet
{
    internal class TheInternetFixture
    {
        private IWebDriver _driver;

        private PagesSpace _ya;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();

            _ya = _driver.Ya(opts => opts.WithBaseUrl("https://the-internet.herokuapp.com")).TheInternet.Pages;
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Quit();
        }

        [Test]
        public void Hover()
        {
            var user = _ya.HoversPage.Open().Users[1].Hover();

            user.Name.Displayed.Should().BeTrue();
            user.Name.Text.Should().Be("name: user2");
        }


        [Test]
        public void ContextClick()
        {
            _ya.ContextMenuPage.Open().Box.RightClick();

            var alert = _driver.SwitchTo().Alert();
            alert.Text.Should().Be("You selected a context menu");
            alert.Accept();
        }

        [Test, Ignore("")]
        // https://github.com/w3c/webdriver/issues/1488
        public void DragAndDrop()
        {
            var page = _ya.DragAndDropPage.Open();

            page.ColumnA.Text.Should().Be("A");
            page.ColumnB.Text.Should().Be("B");

            page.ColumnA.DragAndDrop(page.ColumnB)
                .Text.Should().Be("B");

            page.ColumnB.Text.Should().Be("A");
        }
    }
}
