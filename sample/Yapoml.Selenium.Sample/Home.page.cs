namespace Yapoml.Selenium.Sample;

partial class HomePage
{
    public PackagesPage Search(string text)
    {
        using (_logger.BeginLogScope($"Searching for packages by '{text}' query"))
        {
            SearchInput.Type(text);
            SearchButton.Click();

            return SpaceOptions.Services.Get<YaSpace>().PackagesPage;
        }
    }
}
