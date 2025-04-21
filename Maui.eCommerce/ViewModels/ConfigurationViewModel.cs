using Library.eCommerce.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.eCommerce.ViewModels
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        private decimal _taxRate;
        private CartManagerService _cartManager = CartManagerService.Current;
        
        public decimal TaxRate
        {
            get => _taxRate;
            set
            {
                if (_taxRate != value)
                {
                    _taxRate = value;
                    int activeCartId = CartManagerService.Current.ActiveCartID;
                    _cartManager.CartItems[activeCartId].TaxRate = value / 100.0m; // Convert percentage to decimal
                    NotifyPropertyChanged();
                }
            }
        }
        
        public ConfigurationViewModel()
        {
            int activeCartId = CartManagerService.Current.ActiveCartID;
            _taxRate = _cartManager.CartItems[activeCartId].TaxRate * 100.0m; // Convert decimal to percentage
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}