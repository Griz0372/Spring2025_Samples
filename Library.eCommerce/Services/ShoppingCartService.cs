
using Spring2025_Samples.Models;
using System.Collections.ObjectModel;

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
        
        public void AddToCart(Product product, int quantity)
        {
            // Check if the product is already in the cart
            var existingItem = Items.FirstOrDefault(i => i.Product?.Id == product.Id);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem 
                { 
                    Product = product, 
                    Quantity = quantity 
                });
            }
        }
        
        public void RemoveFromCart(int productId)
        {
            var item = Items.FirstOrDefault(i => i.Product?.Id == productId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }
        
        public void UpdateQuantity(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product?.Id == productId);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    Items.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
            }
        }
        
        public void ClearCart()
        {
            Items.Clear();
        }
        
        public decimal Subtotal => Items.Sum(i => i.Subtotal);
        
        public decimal Tax => Subtotal * TaxRate;
        
        public decimal Total => Subtotal + Tax;
    }
}