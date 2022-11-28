using System.Linq;

namespace Yapoml.Selenium.Sample.Responsive.Pages
{
    partial class BasePage
    {
        public virtual void Navigate(string menuItemName)
        {
            Navigation.MenuItems.First(mi => mi.Title.Text == menuItemName).Click();
        }
    }

    partial class Base1Page
    {
        public override void Navigate(string menuItemName)
        {
            HamburgerButton.Click();

            base.Navigate(menuItemName);
        }
    }
}
