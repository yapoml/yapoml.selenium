using OpenQA.Selenium;
using System;

namespace Yapoml.Selenium.Events.Args.WebElement
{
    public class FoundElementEventArgs : EventArgs
    {
        public FoundElementEventArgs(By by, IWebDriver webDriver, IWebElement webElement)
        {
            By = by;
            WebDriver = webDriver;
            WebElement = webElement;
        }

        public By By { get; }
        public IWebDriver WebDriver { get; }
        public IWebElement WebElement { get; }
    }
}
