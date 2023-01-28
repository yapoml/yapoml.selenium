using System;

namespace Yapoml.Selenium.Sample.Basics.Pages
{
    partial class BaseComponent<TComponent>
    {
        public override void ScrollIntoView()
        {
            Console.WriteLine("I am invoked each time when component is scrolling into view");

            base.ScrollIntoView();
        }
    }
}
