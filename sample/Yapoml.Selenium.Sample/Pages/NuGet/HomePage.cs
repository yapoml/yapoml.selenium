namespace Yapoml.Selenium.Sample.Pages.NuGet
{
    public partial class HomePage
    {
        public void Search(string text)
        {
            SearchInput.SendKeys(text);
            SearchButton.Click();
        }
    }
}
