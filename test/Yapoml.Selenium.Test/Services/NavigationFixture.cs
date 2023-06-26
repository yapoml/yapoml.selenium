using FluentAssertions;
using Moq;
using NUnit.Framework;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Options;
using Yapoml.Selenium.Services;

namespace Yapoml.Selenium.Test.Services
{
    internal class NavigationFixture
    {
        [Test]
        public void BuildUrl_WhichIsAbsolute()
        {
            var spaceOptions = new Mock<ISpaceOptions>();

            var service = new NavigationService(spaceOptions.Object);

            var url = service.BuildUri("https://example.com", null, null);

            url.Should().Be("https://example.com");
        }

        [Test]
        public void BuildUrl2()
        {
            var spaceOptions = new Mock<ISpaceOptions>();
            spaceOptions.Setup(so => so.Services.Get<BaseUrlOptions>()).Returns(new BaseUrlOptions("https://example.com"));

            var service = new NavigationService(spaceOptions.Object);

            var url = service.BuildUri("search?q={text}", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("text", "qwe") }, null);

            url.Should().Be("https://example.com/search?q=qwe");
        }

        [Test]
        public void BuildUrl3()
        {
            var spaceOptions = new Mock<ISpaceOptions>();
            spaceOptions.Setup(so => so.Services.Get<BaseUrlOptions>()).Returns(new BaseUrlOptions("https://example.com"));

            var service = new NavigationService(spaceOptions.Object);

            var url = service.BuildUri("/search?q={text}", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("text", "qwe") }, new List<KeyValuePair<string, string>> { });

            url.Should().Be("https://example.com/search?q=qwe");
        }
    }
}
