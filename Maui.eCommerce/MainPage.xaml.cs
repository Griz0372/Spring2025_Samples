using Maui.eCommerce.ViewModels;
using Library.eCommerce.Services;

namespace Maui.eCommerce
{
    public partial class MainPage : ContentPage
    
    {
        private ShoppingCartManagementViewModel _viewModel;
        
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            //BindingContext = new ShoppingCartManagementViewModel();
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }
        
        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement");
        }
        
        private void CartClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingCart");
        }
        
        private void ConfigClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Configuration");
        }

        private void CartSelected(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;
    
            // Extract the cart number from the button text (e.g., "Cart 1" -> 1)
            if (int.TryParse(buttonText.Replace("Cart ", ""), out int cartId))
            {
                try
                {
                    // If the cart doesn't exist yet, create all necessary carts up to this ID
                    for (int i = 0; i <= cartId; i++)
                    {
                        if (!CartManagerService.Current.CartItems.ContainsKey(i))
                        {
                            ShoppingCartService newCart = new ShoppingCartService();
                            CartManagerService.Current.CartItems.Add(i, newCart);
                        }
                    }
            
                    // Switch to the selected cart
                    CartManagerService.Current.SwitchActiveCart(cartId);
            
                    // Provide feedback to the user
                    DisplayAlert("Cart Selected", $"You are now using {buttonText}", "OK");
                }
                catch (Exception ex)
                {
                    DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
    }
}