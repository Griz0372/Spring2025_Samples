using Spring2025_Samples.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.eCommerce.Services;

public class ShoppingCartManagementViewModel : INotifyPropertyChanged
{
    private ShoppingCartService _selectedCart;
    public ShoppingCartService? SelectedCart
    {
        get => _selectedCart;
        set
        {
            if (_selectedCart != value)
            {
                _selectedCart = value;
                NotifyPropertyChanged();
                Console.WriteLine("SelectedCart function in ShoppingCartManagementViewModel.cs ran");
            }
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}