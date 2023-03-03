using System;

namespace Yapoml.Selenium.Sample.Basics.Pages
{
    partial class BaseComponent<TComponent, TConditions>
    {
        public override TComponent ScrollIntoView()
        {
            Console.WriteLine("I am invoked each time when component is scrolling into view");

            return base.ScrollIntoView();
        }
    }
}
