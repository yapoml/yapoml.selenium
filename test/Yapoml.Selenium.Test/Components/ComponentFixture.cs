using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Test.Components
{
    internal class ComponentFixture
    {
        [Test]
        public void Should_Not_Be_Dispalyed()
        {
            var webDriver = new Mock<IWebDriver>();
            var elementHandler = new Mock<IElementHandler>();
            elementHandler.Setup(e => e.Locate()).Throws(new NoSuchElementException());

            var container = new Mock<IServicesContainer>();
            var spaceOptions = new Mock<ISpaceOptions>();
            spaceOptions.SetupGet(p => p.Services).Returns(container.Object);

            var component = new Mock<BaseComponent>(webDriver.Object, elementHandler.Object, null, spaceOptions.Object);
            component.CallBase = true;

            component.Object.Displayed.Should().BeFalse();
        }
    }
}
