using OpenQA.Selenium;
using System;
using Yapoml.Framework.Options;
using Yapoml.Selenium.Components;
using Yapoml.Selenium.Services.Locator;

namespace Yapoml.Selenium.Services.Factory
{
    public class DefaultPageFactory : IPageFactory
    {
        public TPage Create<TPage>(IWebDriver webDriver, IElementHandlerRepository elementHandlerRepository, ISpaceOptions spaceOptions) where TPage : BasePage
        {
            var page = (TPage)Activator.CreateInstance(typeof(TPage), webDriver, elementHandlerRepository, spaceOptions);

            return page;
        }
    }
}
