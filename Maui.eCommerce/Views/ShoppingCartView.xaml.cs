using Maui.eCommerce.ViewModels;
using Spring2025_Samples.Models;

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
            }
        }
        
        private void IncreaseQuantity_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cartItem = (CartItem)button.BindingContext;
            
            if (cartItem?.Product != null)
            {
                _viewModel.UpdateQuantity(cartItem.Product.Id, cartItem.Quantity + 1);
            }
        }
        
        private void DecreaseQuantity_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cartItem = (CartItem)button.BindingContext;
            
            if (cartItem?.Product != null && cartItem.Quantity > 1)
            {
                _viewModel.UpdateQuantity(cartItem.Product.Id, cartItem.Quantity - 1);
            }
        }
        
        private void ClearCart_Clicked(object sender, EventArgs e)
        {
            _viewModel.ClearCart();
        }
        
        private void Checkout_Clicked(object sender, EventArgs e)
        {
            // Implement checkout logic here
            DisplayAlert("Success", "Your order has been placed!", "OK");
            _viewModel.ClearCart();
        }
        
        private void ContinueShopping_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}