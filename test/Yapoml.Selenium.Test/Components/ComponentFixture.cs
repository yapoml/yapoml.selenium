using FluentAssertions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Components.Metadata;
using Yapoml.Selenium.Events;
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

            var component = new Mock<BaseComponent<TestComponent, TestComponent.TestConditions>>(webDriver.Object, elementHandler.Object, null, spaceOptions.Object);
            component.CallBase = true;

            component.Object.Displayed.Should().BeFalse();
        }
    }

    public class TestComponent : BaseComponent<TestComponent, TestComponent.TestConditions>
    {
        public TestComponent(IWebDriver webDriver, IElementHandler elementHandler, ComponentMetadata metadata, ISpaceOptions spaceOptions)
            : base(webDriver, elementHandler, metadata, spaceOptions)
        {
        }

        public class TestConditions : BaseComponentConditions<TestConditions>
        {
            public TestConditions(TimeSpan timeout, TimeSpan pollingInterval, IWebDriver webDriver, IElementHandler elementHandler, IElementLocator elementLocator, IEventSource eventSource)
                : base(timeout, pollingInterval, webDriver, elementHandler, elementLocator, eventSource)
            {
            }
        }
    }
}
