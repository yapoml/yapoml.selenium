using OpenQA.Selenium;
using System;
using System.Drawing;
using Yapoml.Options;
using Yapoml.Selenium.Events;

namespace Yapoml.Selenium
{
    public static class LighterExtension
    {
        private static int _delay;

        private static int _fadeOutSpeed;

        private static Color _color = Color.FromArgb(70, 255, 255, 0);

        public static ISpaceOptions UseLighter(this ISpaceOptions spaceOptions, int delay = 200, int fadeOutSpeed = 200, Color color = default)
        {
            _delay = delay;
            _fadeOutSpeed = fadeOutSpeed;

            if (color != default)
            {
                _color = color;
            }

            var eventSource = spaceOptions.Get<IEventSource>();

            eventSource.ComponentEventSource.OnFoundComponent += ComponentEventSource_OnFoundComponent;

            return spaceOptions;
        }

        private static void ComponentEventSource_OnFoundComponent(object sender, Events.Args.WebElement.FoundElementEventArgs e)
        {
            var jsExecutor = e.WebDriver as IJavaScriptExecutor;

            if (jsExecutor != null)
            {
                try
                {
                    var backgroundColor = e.WebElement.GetCssValue("backgroundColor");

                    jsExecutor.ExecuteScript($"arguments[0].setAttribute('style', 'background: rgba({_color.R}, {_color.G}, {_color.B}, {(float)_color.A / 100});');", e.WebElement);

                    System.Threading.Thread.Sleep(_delay);

                    jsExecutor.ExecuteScript($"arguments[0].setAttribute('style', 'background: {backgroundColor}; transition: all {_fadeOutSpeed}ms ease-in-out;');", e.WebElement);

                    
                }
                catch (Exception) { }
            }
        }
    }
}
