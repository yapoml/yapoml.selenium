namespace Yapoml.Selenium.Sample.Basics.Pages
{
    partial class HomePage
    {
        public PackagesPage Search(string text)
        {
            SearchInput.Type(text);
            SearchButton.Click();

            return SpaceOptions.Services.Get<YaSpace>().Basics.Pages.PackagesPage;
        }
    }
}
