namespace Yapoml.Selenium.Sample
{
    partial class HomePage
    {
        public PackagesPage Search(string text)
        {
            SearchInput.Type(text);
            SearchButton.Click();

            return SpaceOptions.Services.Get<YaSpace>().PackagesPage;
        }
    }
}
