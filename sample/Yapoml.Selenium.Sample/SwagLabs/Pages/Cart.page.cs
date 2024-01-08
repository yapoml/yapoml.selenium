namespace Yapoml.Selenium.Sample.SwagLabs.Pages;

partial class CartPage
{
    public CartPage RemoveAllItems()
    {
        using (_logger.BeginLogScope("Removing all items from cart"))
        {
            Items.ForEach(i => i.RemoveButton.Click());
        }

        return this;
    }
}
