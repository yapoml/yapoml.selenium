using OpenQA.Selenium;
using System;
using Yapoml.Options;
using Yapoml.Selenium.Components;

namespace Yapoml.Selenium.Factory
{
    public class DefaultPageFactory : IPageFactory
    {
        public TPage Create<TPage>(IWebDriver webDriver, ISpaceOptions spaceOptions) where TPage : BasePage
        {
            var page = (TPage)Activator.CreateInstance(typeof(TPage), webDriver, spaceOptions);

            return page;
        }
    }
}
