namespace Yapoml.Selenium.Sample.Responsive.Pages
{
    public partial class HomeMobilePage
    {
        public override void Navigate(string menuItemName)
        {
            HamburgerButton.Click();

            base.Navigate(menuItemName);
        }
    }
}
