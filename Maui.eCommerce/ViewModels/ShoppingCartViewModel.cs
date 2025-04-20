using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        private string _sortOption = "Name";
        private CartItem? _selectedItem;
        
        public ObservableCollection<CartItem> Items 
        { 
            get 
            {
                var items = ShoppingCartService.Current.Items;
                
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
        
        public decimal Subtotal => ShoppingCartService.Current.Subtotal;
        public decimal Tax => ShoppingCartService.Current.Tax;
        public decimal Total => ShoppingCartService.Current.Total;
        
        public void RemoveItem()
        {
            if (SelectedItem?.Product != null)
            {
                ShoppingCartService.Current.RemoveFromCart(SelectedItem.Product.Id);
                RefreshCart();
            }
        }
        
        public void UpdateQuantity(int productId, int quantity)
        {
            ShoppingCartService.Current.UpdateQuantity(productId, quantity);
            RefreshCart();
        }
        
        public void ClearCart()
        {
            ShoppingCartService.Current.ClearCart();
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