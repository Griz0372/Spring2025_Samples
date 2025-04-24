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
            var quantityEntry = button.Parent.FindByName<Entry>("QuantityEntry");
            int activeCartId = CartManagerService.Current.ActiveCartID;
            
            if (quantityEntry != null && int.TryParse(quantityEntry.Text, out int quantity) && quantity > 0)
            {
                if (product.StockQuantity < quantity)
                {
                    DisplayAlert("Error", $"Not enough stock available. Only {product.StockQuantity} items available.", "OK");
                    return;
                }
                
                bool success = CartManagerService.Current.CartItems[activeCartId].AddToCart(product, quantity);
                
                if (success)
                {
                    quantityEntry.Text = string.Empty;
                    DisplayAlert("Success", $"{quantity} x {product.Name} added to cart", "OK");
                    _viewModel.RefreshProductList(); // Refresh the list to show updated quantities
                }
                else
                {
                    DisplayAlert("Error", $"Not enough stock available. Only {product.StockQuantity} items available.", "OK");
                }
            }
            else
            {
                if (product.StockQuantity < 1)
                {
                    DisplayAlert("Error", "This item is out of stock.", "OK");
                    return;
                }
                
                bool success = CartManagerService.Current.CartItems[activeCartId].AddToCart(product, 1);
                
                if (success)
                {
                    DisplayAlert("Success", $"1 x {product.Name} added to cart", "OK");
                    _viewModel.RefreshProductList(); // Refresh the list to show updated quantities
                }
                else
                {
                    DisplayAlert("Error", "This item is out of stock.", "OK");
                }
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