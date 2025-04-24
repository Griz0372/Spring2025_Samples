
using Spring2025_Samples.Models;
using System.Collections.ObjectModel;
using Library.eCommerce.Services;

namespace Library.eCommerce.Services
{
    public class ShoppingCartService
    {
        public ShoppingCartService()
        {
            Items = new List<CartItem>();
        }
        
        public List<CartItem> Items { get; private set; }
        public decimal TaxRate { get; set; } = 0.0m; // Default tax rate
        
        public bool AddToCart(Product product, int quantity)
        {
            var currentProduct = ProductServiceProxy.Current.GetById(product.Id);
            
            if (currentProduct == null || currentProduct.StockQuantity < quantity)
            {
                
                return false;
            }
            
            var existingItem = Items.FirstOrDefault(i => i.Product?.Id == product.Id);
            
            if (existingItem != null)
            {
                if (currentProduct.StockQuantity < quantity)
                {
                    return false;
                }
                
                existingItem.Quantity += quantity;
                
                currentProduct.StockQuantity -= quantity;
            }
            else
            {
                if (currentProduct.StockQuantity < quantity)
                {
                    return false;
                }
                
                Items.Add(new CartItem 
                { 
                    Product = product, 
                    Quantity = quantity 
                });
                
                currentProduct.StockQuantity -= quantity;
            }
            
            return true;
        }
        
        public void RemoveFromCart(int productId)
        {
            var item = Items.FirstOrDefault(i => i.Product?.Id == productId);
            if (item != null)
            {
                var product = ProductServiceProxy.Current.GetById(productId);
                if (product != null)
                {
                    product.StockQuantity += item.Quantity;
                }
                
                Items.Remove(item);
            }
        }
        
        public void UpdateQuantity(int productId, int newQuantity)
        {
            var item = Items.FirstOrDefault(i => i.Product?.Id == productId);
            if (item != null)
            {
                var product = ProductServiceProxy.Current.GetById(productId);
                if (product != null)
                {
                    if (newQuantity <= 0)
                    {
                        product.StockQuantity += item.Quantity;
                        Items.Remove(item);
                    }
                    else
                    {
                        int quantityDifference = newQuantity - item.Quantity;
                        
                        if (quantityDifference > 0)
                        {
                            if (product.StockQuantity >= quantityDifference)
                            {
                                product.StockQuantity -= quantityDifference;
                                item.Quantity = newQuantity;
                            }
                        }
                        else if (quantityDifference < 0)
                        {
                            product.StockQuantity += Math.Abs(quantityDifference);
                            item.Quantity = newQuantity;
                        }
                    }
                }
            }
        }
        
        public void ClearCart()
        {
            foreach (var item in Items)
            {
                if (item.Product != null)
                {
                    var product = ProductServiceProxy.Current.GetById(item.Product.Id);
                    if (product != null)
                    {
                        product.StockQuantity += item.Quantity;
                    }
                }
            }
            
            Items.Clear();
        }
        
        public decimal Subtotal => Items.Sum(i => i.Subtotal);
        
        public decimal Tax => Subtotal * TaxRate;
        
        public decimal Total => Subtotal + Tax;
    }
}