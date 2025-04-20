using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;
using Spring2025_Samples.Models;

namespace Maui.eCommerce.Views;

public partial class InventoryManagementView : ContentPage
{
    private InventoryManagementViewModel _viewModel;
    
    public InventoryManagementView()
    {
        InitializeComponent();
        _viewModel = new InventoryManagementViewModel();
        BindingContext = _viewModel;
    }

    private void DeleteItem_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var product = (Product)button.BindingContext;
        
        if (product != null)
        {
            ProductServiceProxy.Current.Delete(product.Id);
            _viewModel.RefreshProductList();
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        _viewModel.RefreshProductList();
    }

    private void EditItem_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var product = (Product)button.BindingContext;
        
        if (product != null)
        {
            Shell.Current.GoToAsync($"//Product?productId={product.Id}");
        }
    }

    private void AddToCart_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var product = (Product)button.BindingContext;
        
        if (product != null)
        {
            // Find the quantity entry for this product
            var quantityEntry = button.Parent.FindByName<Entry>("QuantityEntry");
            
            if (quantityEntry != null && int.TryParse(quantityEntry.Text, out int quantity) && quantity > 0)
            {
                ShoppingCartService.Current.AddToCart(product, quantity);
                quantityEntry.Text = string.Empty;
                DisplayAlert("Success", $"{quantity} x {product.Name} added to cart", "OK");
            }
            else
            {
                // Default to adding 1 item if no valid quantity is specified
                ShoppingCartService.Current.AddToCart(product, 1);
                DisplayAlert("Success", $"1 x {product.Name} added to cart", "OK");
            }
        }
    }

    private void ViewCartClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ShoppingCart");
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        _viewModel.RefreshProductList();
    }
}