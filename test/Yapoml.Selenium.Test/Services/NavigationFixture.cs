using FluentAssertions;
using NUnit.Framework;
using Yapoml.Selenium.Services;

namespace Yapoml.Selenium.Test.Services
{
    internal class NavigationFixture
    {
        [Test]
        public void BuildUrl()
        {
            var service = new NavigationService("https://example.com");

            var url = service.BuildUri("/search?q={text}", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("text", "qwe") }, null);

            url.Should().Be("https://example.com/search?q=qwe");
        }

        [Test]
        public void BuildUrl2()
        {
            var service = new NavigationService("https://example.com");

            var url = service.BuildUri("/search?q={text}", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("text", "qwe") }, new List<KeyValuePair<string, string>> { });

            url.Should().Be("https://example.com/search?q=qwe");
        }
    }
}
