using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.Views
{
    public partial class ConfigurationView : ContentPage
    {
        public ConfigurationView()
        {
            InitializeComponent();
            BindingContext = new ConfigurationViewModel();
        }
        
        private void SaveClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }
    }
}