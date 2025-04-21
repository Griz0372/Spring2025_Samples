using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce
{
    public partial class MainPage : ContentPage
    
    {
        private ShoppingCartManagementViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            BindingContext = new ShoppingCartManagementViewModel();
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
            Console.WriteLine("Cart Selected");
            //Dont know what to do from here
        }
    }
}