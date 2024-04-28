namespace Yapoml.Selenium.Sample.SwagLabs.Pages;

partial class CartPage
{
    public CartPage RemoveAllItems()
    {
        using (var scope = _logger.BeginLogScope("Removing all items from cart"))
        {
            scope.Execute(() =>
            {
                Items.ForEach(i => i.RemoveButton.Click());
            });
        }

        return this;
    }
}
