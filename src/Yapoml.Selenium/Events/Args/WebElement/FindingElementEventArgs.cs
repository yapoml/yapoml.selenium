using OpenQA.Selenium;
using System;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FindingElementEventArgs : EventArgs
    {
        public FindingElementEventArgs(string componentName, By by)
        {
            ComponentName = componentName;
            By = by;
        }

        public string ComponentName { get; }
        public By By { get; }
    }
}
