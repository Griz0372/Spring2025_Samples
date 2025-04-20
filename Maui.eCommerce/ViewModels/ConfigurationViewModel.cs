using Library.eCommerce.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.eCommerce.ViewModels
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        private decimal _taxRate;
        
        public decimal TaxRate
        {
            get => _taxRate;
            set
            {
                if (_taxRate != value)
                {
                    _taxRate = value;
                    ShoppingCartService.Current.TaxRate = value / 100.0m; // Convert percentage to decimal
                    NotifyPropertyChanged();
                }
            }
        }
        
        public ConfigurationViewModel()
        {
            _taxRate = ShoppingCartService.Current.TaxRate * 100.0m; // Convert decimal to percentage
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}