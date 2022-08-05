using FluentAssertions;
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

            act.Should().Throw<TimeoutException>();
        }
    }
}
