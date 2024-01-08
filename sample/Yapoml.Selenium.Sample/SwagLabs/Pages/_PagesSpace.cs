namespace Yapoml.Selenium.Sample.SwagLabs.Pages;

partial class PagesSpace
{
    public InventoryPage Login(string username, string password)
    {
        LoginPage.Open().Form
            .Username.Type(username)
            .Password.Type(password)
            .Login.Click();

        return InventoryPage.Expect().IsOpened();
    }
}
