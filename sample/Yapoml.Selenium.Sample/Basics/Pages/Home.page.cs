﻿namespace Yapoml.Selenium.Sample.Basics.Pages
{
    partial class HomePage
    {
        public void Search(string text)
        {
            SearchInput.SendKeys(text);
            SearchButton.Click();
        }
    }
}