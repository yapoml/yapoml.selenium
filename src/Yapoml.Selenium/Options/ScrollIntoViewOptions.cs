namespace Yapoml.Selenium.Options
{
    public class ScrollIntoViewOptions
    {
        /// <summary>
        /// Defines the transition animation.
        /// Default <see cref="ScrollIntoViewBehavior.Auto" />
        /// </summary>
        public ScrollIntoViewBehavior Behavior { get; set; } = ScrollIntoViewBehavior.Auto;

        /// <summary>
        /// Defines vertical alignment.
        /// Default <see cref="ScrollIntoViewBlock.Start" />
        /// </summary>
        public ScrollIntoViewBlock Block { get; set; } = ScrollIntoViewBlock.Start;

        /// <summary>
        /// Defines horizontal alignment.
        /// Default <see cref="ScrollIntoViewInline.Nearest" />
        /// </summary>
        public ScrollIntoViewInline Inline { get; set; } = ScrollIntoViewInline.Nearest;

        public string ToJson()
        {
            return $"{{behavior: \"{Behavior.ToString().ToLowerInvariant()}\", block: \"{Block.ToString().ToLowerInvariant()}\", inline: \"{Inline.ToString().ToLowerInvariant()}\"}}";
        }

        public override string ToString()
        {
            return $"Behavior: {Behavior}, Vertical aligment: {Block}, Horizontal aligment: {Inline}";
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
