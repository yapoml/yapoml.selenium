using FluentAssertions;
using Moq;
using OpenQA.Selenium;
using Yapoml.Selenium.Services;

namespace Yapoml.Selenium.Test
{
    class WaiterFixture
    {
        [Test]
        public void Should_Execute_Func()
        {
            var res = Waiter.Until(() => true, TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(100));

            res.Should().BeTrue();
        }

        [Test]
        public void Should_Timeout()
        {
            Action act = () => Waiter.Until<object>(() => throw new Exception(), TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(10));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void Should_Wait_Until_Component_Displayed()
        {
            var searchContextMock = new Mock<ISearchContext>();
            var webElementMock = new Mock<IWebElement>();
            webElementMock.Setup(e => e.Displayed).Returns(false);
            searchContextMock.Setup(sc => sc.FindElement(It.IsAny<By>())).Returns(webElementMock.Object);

            Action act = () => Waiter.UntilComponentDisplayed("my", searchContextMock.Object, By.Id(""), TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(5));
             
            var ex = act.Should().Throw<TimeoutException>().Subject.First();
            Console.WriteLine(ex);
        }

        [Test]
        public void Should_Wait_Until_Component_WithIgnored_NoSuchElementException()
        {
            var searchContextMock = new Mock<ISearchContext>();
            searchContextMock.Setup(sc => sc.FindElement(It.IsAny<By>())).Throws<NoSuchElementException>();

            Action act = () => Waiter.UntilComponentDisplayed("my", searchContextMock.Object, By.Id(""), TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(5));

            var ex = act.Should().Throw<TimeoutException>().Subject.First();
            Console.WriteLine(ex);
        }

        [Test]
        public void Should_Wait_Until_Component_WithIgnored_StaleReferenceException()
        {
            var searchContextMock = new Mock<ISearchContext>();
            searchContextMock.Setup(sc => sc.FindElement(It.IsAny<By>())).Throws<StaleElementReferenceException>();

            Action act = () => Waiter.UntilComponentDisplayed("my", searchContextMock.Object, By.Id(""), TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(5));

            var ex = act.Should().Throw<TimeoutException>().Subject.First();
            Console.WriteLine(ex);
        }

        [Test]
        public void Should_Wait_Until_Displayed()
        {
            var elementMock = new Mock<IWebElement>();
            elementMock.Setup(e => e.Displayed).Returns(false);

            Action act = () => Waiter.UntilDisplayed("my", elementMock.Object, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(5));
            act.Should().Throw<TimeoutException>();
        }

        [Test]
        public void Should_Wait_Until_Enabled()
        {
            var elementMock = new Mock<IWebElement>();
            elementMock.Setup(e => e.Enabled).Returns(false);

            Action act = () => Waiter.UntilEnabled("my", elementMock.Object, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(5));
            act.Should().Throw<TimeoutException>();
        }
    }
}
