using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        private CartManagerService _cartManager = CartManagerService.Current;
        private int _selectedCartId;
        
        private string _sortOption = "Name";
        private CartItem? _selectedItem;
        
        private ObservableCollection<int> _availableCarts;

        public ObservableCollection<int> AvailableCarts
        {
            get => _availableCarts;
            set
            {
                _availableCarts = value;
                NotifyPropertyChanged();
            }
        }

        public int SelectedCartId
        {
            get => _selectedCartId;
            set
            {
                _selectedCartId = value;
                Console.WriteLine("Selected Cart ID is: " + _selectedCartId);
                NotifyPropertyChanged();
            }
        }
        
        
        public ObservableCollection<CartItem> Items 
        { 
            get 
            {
                int activeCartId = CartManagerService.Current.ActiveCartID;
        
                if (!CartManagerService.Current.CartItems.TryGetValue(activeCartId, out var activeCart))
                {
                    return new ObservableCollection<CartItem>();
                }
        
                var items = activeCart.Items;
        
                // Apply sorting if needed
                if (_sortOption == "Name")
                {
                    items = items.OrderBy(i => i.Product?.Name).ToList();
                }
                else if (_sortOption == "Price")
                {
                    items = items.OrderBy(i => i.Product?.Price).ToList();
                }
        
                return new ObservableCollection<CartItem>(items);
            }
        }
        
        public CartItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public string SortOption
        {
            get => _sortOption;
            set
            {
                if (_sortOption != value)
                {
                    _sortOption = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Items));
                }
            }
        }
        
        
        public decimal Subtotal => _cartManager.CartItems[_cartManager.ActiveCartID].Subtotal;
        public decimal Tax => _cartManager.CartItems[_cartManager.ActiveCartID].Tax;
        public decimal Total => _cartManager.CartItems[_cartManager.ActiveCartID].Total;
        
        public void RemoveItem()
        {
            if (SelectedItem?.Product != null)
            {
                int activeCartId = CartManagerService.Current.ActiveCartID;
                _cartManager.CartItems[activeCartId].RemoveFromCart(SelectedItem.Product.Id);
                RefreshCart();
            }
        }
        
        public void UpdateQuantity(int productId, int quantity)
        {
            int activeCartId = CartManagerService.Current.ActiveCartID;
            _cartManager.CartItems[activeCartId].UpdateQuantity(productId, quantity);
            RefreshCart();
        }
        
        public void ClearCart()
        {
            int activeCartId = CartManagerService.Current.ActiveCartID;
            _cartManager.CartItems[activeCartId].ClearCart();
            RefreshCart();
        }
        
        public void RefreshCart()
        {
            NotifyPropertyChanged(nameof(Items));
            NotifyPropertyChanged(nameof(Subtotal));
            NotifyPropertyChanged(nameof(Tax));
            NotifyPropertyChanged(nameof(Total));
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}