namespace Yapoml.Selenium.Options
{
    public class ScrollIntoViewOptions
    {
        /// <summary>
        /// Defines the transition animation.
        /// </summary>
        public ScrollIntoViewBehavior Behavior { get; set; } = ScrollIntoViewBehavior.Auto;

        /// <summary>
        /// Defines vertical alignment.
        /// </summary>
        public ScrollIntoViewBlock Block { get; set; } = ScrollIntoViewBlock.Start;

        /// <summary>
        /// Defines horizontal alignment.
        /// </summary>
        public ScrollIntoViewInline Inline { get; set; } = ScrollIntoViewInline.Start;

        public override string ToString()
        {
            return $"{{behavior: \"{Behavior.ToString().ToLowerInvariant()}\", block: \"{Block.ToString().ToLowerInvariant()}\", inline: \"{Inline.ToString().ToLowerInvariant()}\"}}";
        }
    }

    public enum ScrollIntoViewBehavior
    {
        Auto,
        Smooth
    }

    public enum ScrollIntoViewBlock
    {
        Start,
        Center,
        End,
        Nearest
    }

    public enum ScrollIntoViewInline
    {
        Start,
        Center,
        End,
        Nearest
    }
}
