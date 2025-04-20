using Library.eCommerce.Services;
using Spring2025_Samples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    // Update Maui.eCommerce/ViewModels/ProductViewModel.cs
    public class ProductViewModel
    {
        public string? Name { 
            get
            {
                return Model?.Name ?? string.Empty;
            }

            set
            {
                if(Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }
    
        public decimal Price { 
            get
            {
                return Model?.Price ?? 0.0m;
            }

            set
            {
                if(Model != null && Model.Price != value)
                {
                    Model.Price = value;
                }
            }
        }
    
        public int StockQuantity { 
            get
            {
                return Model?.StockQuantity ?? 0;
            }

            set
            {
                if(Model != null && Model.StockQuantity != value)
                {
                    Model.StockQuantity = value;
                }
            }
        }

        public Product? Model { get; set; }

        public void AddOrUpdate()
        {
            ProductServiceProxy.Current.AddOrUpdate(Model);
        }

        public ProductViewModel() {
            Model = new Product();
        }

        public ProductViewModel(Product? model)
        {
            Model = model;
        }
    }
}
