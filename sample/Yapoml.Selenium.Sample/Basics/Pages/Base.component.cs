using System;

namespace Yapoml.Selenium.Sample.Basics.Pages
{
    partial class BaseComponent<TComponent, TConditions, TCondition>
    {
        public override TComponent ScrollIntoView()
        {
            Console.WriteLine($"I am invoked each time when {Metadata.Name} component is scrolling into view");

            return base.ScrollIntoView();
        }
    }
}
