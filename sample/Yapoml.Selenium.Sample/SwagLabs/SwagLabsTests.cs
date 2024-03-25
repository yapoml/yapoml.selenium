using NUnit.Framework;
using Yapoml.Selenium.Sample.SwagLabs.Pages;

namespace Yapoml.Selenium.Sample.SwagLabs
{
    class SwagLabsTests : BaseTest
    {
        private PagesSpace _ya;

        [SetUp]
        public void SetUp()
        {
            _ya = _webDriver.Ya(opts => opts.WithBaseUrl("https://www.saucedemo.com")).SwagLabs.Pages;
        }

        [Test]
        public void IncorrectLogin()
        {
            var error = _ya.LoginPage.Open().Form
                .Login.Click().Error;

            error.Expect(it => it.IsDisplayed().Text.Is("Epic sadface: Username is required"));

            error.Close.Click();

            error.Expect().IsNotDisplayed();
        }

        [Test]
        public void AddToCart()
        {
            _ya.Login("standard_user", "secret_sauce")
                .Products[p => p.Name == "Sauce Labs Backpack"].AddToCartButton.Click();

            _ya.InventoryPage.PrimaryHeader.ShoppingCart
                .Expect(its => its.Badge.Is("1").Styles.BackgroundColor.Is("rgba(226, 35, 26, 1)"))
                .Click();

            _ya.CartPage.Expect().IsOpened()
                .Items.Expect().Count.Is(1);

            _ya.CartPage.RemoveAllItems()
                .Expect(its => its.Items.IsEmpty())
                .Expect(its => its.PrimaryHeader.ShoppingCart.Badge.IsNotDisplayed());
        }
    }
}
