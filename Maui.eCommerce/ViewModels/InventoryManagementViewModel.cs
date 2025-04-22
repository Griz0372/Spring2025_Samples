using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        private string _sortOption = "Name";
        public Product? SelectedProduct { get; set; }
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;

        public string SortOption
        {
            get => _sortOption;
            set
            {
                if (_sortOption != value)
                {
                    _sortOption = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Products));
                }
            }
        }

        public ObservableCollection<Product?> Products
        {
            get
            {
                var filteredList = _svc.Products.Where(p =>
                    p?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);

                // Apply sorting
                if (_sortOption == "Name")
                {
                    filteredList = filteredList.OrderBy(p => p?.Name);
                }
                else if (_sortOption == "Price")
                {
                    filteredList = filteredList.OrderBy(p => p?.Price);
                }
                return new ObservableCollection<Product?>(filteredList);
            }
        }
        
        public Product? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Products");
            return item;
        }

        
        public event PropertyChangedEventHandler? PropertyChanged;
        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
