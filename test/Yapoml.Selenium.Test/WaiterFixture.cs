using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using Yapoml.Selenium.Services;
using Yapoml.Selenium.Services.Locator;

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

        //[Test]
        //public void Should_Wait_Until_Component_Displayed()
        //{
        //    var webElementMock = new Mock<IWebElement>();
        //    webElementMock.Setup(e => e.Displayed).Returns(false);

        //    var elementHandler = new Mock<IElementHandler>();
        //    elementHandler.Setup(h => h.Locate()).Returns(webElementMock.Object);
        //    elementHandler.Setup(h => h.ComponentMetadata).Returns(new Selenium.Components.Metadata.ComponentMetadata { });

        //    Action act = () => Waiter.UntilDisplayed(elementHandler.Object, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(5));

        //    var ex = act.Should().Throw<TimeoutException>().Subject.First();
        //    Console.WriteLine(ex);
        //}

        //[Test]
        //public void Should_Wait_Until_Component_WithIgnored_NoSuchElementException()
        //{
        //    var webElementMock = new Mock<IWebElement>();
        //    webElementMock.Setup(e => e.Displayed).Returns(false);

        //    var elementHandler = new Mock<IElementHandler>();
        //    elementHandler.Setup(h => h.Locate()).Throws<NoSuchElementException>();
        //    elementHandler.Setup(h => h.ComponentMetadata).Returns(new Selenium.Components.Metadata.ComponentMetadata { });

        //    Action act = () => Waiter.UntilDisplayed(elementHandler.Object, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(5));

        //    var ex = act.Should().Throw<TimeoutException>().Subject.First();
        //    Console.WriteLine(ex);
        //}

        //[Test]
        //public void Should_Wait_Until_Component_WithIgnored_StaleReferenceException()
        //{
        //    var webElementMock = new Mock<IWebElement>();
        //    webElementMock.Setup(e => e.Displayed).Returns(false);

        //    var elementHandler = new Mock<IElementHandler>();
        //    elementHandler.Setup(h => h.Locate()).Throws<StaleElementReferenceException>();
        //    elementHandler.Setup(h => h.ComponentMetadata).Returns(new Selenium.Components.Metadata.ComponentMetadata { });

        //    Action act = () => Waiter.UntilDisplayed(elementHandler.Object, TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(5));

        //    var ex = act.Should().Throw<TimeoutException>().Subject.First();
        //    Console.WriteLine(ex);
        //}
    }
}
