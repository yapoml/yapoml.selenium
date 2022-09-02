using System.Linq;

namespace Yapoml.Selenium.Sample.Responsive.Pages
{
    public partial class HomePage
    {
        public virtual void Navigate(string menuItemName)
        {
            Navigation.MenuItems.First(mi => mi.Title.Text == menuItemName).Click();
        }
    }

    public partial class HomePage1
    {
        public override void Navigate(string menuItemName)
        {
            HamburgerButton.Click();

            base.Navigate(menuItemName);
        }
    }
}
