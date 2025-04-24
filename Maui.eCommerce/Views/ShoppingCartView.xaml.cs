using Maui.eCommerce.ViewModels;
using Spring2025_Samples.Models;
using Library.eCommerce.Services;

namespace Maui.eCommerce.Views
{
    public partial class ShoppingCartView : ContentPage
    {
        private ShoppingCartViewModel _viewModel;
        
        public ShoppingCartView()
        {
            InitializeComponent();
            _viewModel = new ShoppingCartViewModel();
            BindingContext = _viewModel;
        }
        
        private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
        {
            _viewModel.RefreshCart();
        }
        
        private void RemoveItem_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cartItem = (CartItem)button.BindingContext;
            
            if (cartItem?.Product != null)
            {
                _viewModel.UpdateQuantity(cartItem.Product.Id, 0);
                _viewModel.RefreshCart();
            }
        }
        
        private void IncreaseQuantity_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cartItem = (CartItem)button.BindingContext;
            
            if (cartItem?.Product != null)
            {
                var currentProduct = ProductServiceProxy.Current.GetById(cartItem.Product.Id);
                
                if (currentProduct != null && currentProduct.StockQuantity > 0)
                {
                    _viewModel.UpdateQuantity(cartItem.Product.Id, cartItem.Quantity + 1);
                    _viewModel.RefreshCart();
                }
                else
                {
                    DisplayAlert("Error", "No more stock available for this item.", "OK");
                }
            }
        }
        
        private void DecreaseQuantity_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cartItem = (CartItem)button.BindingContext;
            
            if (cartItem?.Product != null && cartItem.Quantity > 1)
            {
                _viewModel.UpdateQuantity(cartItem.Product.Id, cartItem.Quantity - 1);
                _viewModel.RefreshCart();
            }
        }
        
        private void ClearCart_Clicked(object sender, EventArgs e)
        {
            _viewModel.ClearCart();
            _viewModel.RefreshCart();
        }
        
        private void Checkout_Clicked(object sender, EventArgs e)
        {
            bool allItemsAvailable = true;
            string unavailableItems = "";
            
            int activeCartId = CartManagerService.Current.ActiveCartID;
            var cartItems = CartManagerService.Current.CartItems[activeCartId].Items;
            
            foreach (var item in cartItems)
            {
                if (item.Product != null)
                {
                    var currentProduct = ProductServiceProxy.Current.GetById(item.Product.Id);
                    if (currentProduct == null || currentProduct.StockQuantity < 0)
                    {
                        allItemsAvailable = false;
                        unavailableItems += $"• {item.Product.Name}\n";
                    }
                }
            }
            
            if (!allItemsAvailable)
            {
                DisplayAlert("Checkout Failed", 
                    $"The following items are no longer available in the requested quantities:\n{unavailableItems}", 
                    "OK");
                return;
            }
            
           
            DisplayAlert("Success", "Your order has been placed!", "OK");
            _viewModel.ClearCart();
            _viewModel.RefreshCart();
        }
        
        private void ContinueShopping_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }
    }
}