using FluentAssertions;
using NUnit.Framework;
using Yapoml.Selenium.Sample.TheInternet.Pages;

namespace Yapoml.Selenium.Sample.TheInternet
{
    class TheInternetFixture : BaseTest
    {
        private PagesSpace _ya;

        [SetUp]
        public void SetUp()
        {
            _ya = _webDriver.Ya(opts => opts.WithBaseUrl("https://the-internet.herokuapp.com")).TheInternet.Pages;
        }

        [Test]
        public void Hover()
        {
            var user = _ya.HoversPage.Open().Users[1].Hover();

            user.Name.IsDisplayed.Should().BeTrue();
            user.Name.Text.Should().Be("name: user2");
        }


        [Test]
        public void ContextClick()
        {
            _ya.ContextMenuPage.Open().Box.ContextClick();

            var alert = _webDriver.SwitchTo().Alert();
            alert.Text.Should().Be("You selected a context menu");
            alert.Accept();
        }

        [Test, Ignore("https://github.com/w3c/webdriver/issues/1488")]
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
