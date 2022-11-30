using FluentAssertions;
using NUnit.Framework;
using Yapoml.Selenium.Options;

namespace Yapoml.Selenium.Test
{
    class OptionsFixture
    {
        [Test]
        public void DefaultScrollIntoViewOptions()
        {
            var options = new ScrollIntoViewOptions();

            options.Behavior.Should().Be(ScrollIntoViewBehavior.Auto);
            options.Block.Should().Be(ScrollIntoViewBlock.Start);
            options.Inline.Should().Be(ScrollIntoViewInline.Nearest);
        }
    }
}
