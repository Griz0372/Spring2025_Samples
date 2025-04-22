
using Spring2025_Samples.Models;
namespace Library.eCommerce.Services;

public class CartManagerService
{
    private static CartManagerService? _instance;
    private static object _instanceLock = new object();
    public static CartManagerService Current
    {
        get
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new CartManagerService();
                }
            }
            return _instance;
        }
    }
    
    public Dictionary<int, ShoppingCartService> CartItems { get; private set; } = new Dictionary<int, ShoppingCartService>();
    public int ActiveCartID { get; set; }
    public static int numberOfCarts { get; set; }
    private CartManagerService()
    {
        CartItems = new Dictionary<int, ShoppingCartService>();
        CartItems.Add(0, new ShoppingCartService());
        CartItems.Add(1, new ShoppingCartService());
        
        
        numberOfCarts = 2;
        ActiveCartID = 0;
    }

    public void SwitchActiveCart(int cartID)
    {
        if (CartItems.ContainsKey(cartID))
        {
            ActiveCartID = cartID;
        }
        else
        {
            throw new Exception("Cart ID not found");
        }
    }

    public void CreateNewCart()
    {
        ShoppingCartService newShoppingCartService = new ShoppingCartService();
        CartItems.Add(numberOfCarts++, newShoppingCartService);
        
    }
    
    public void PrintAllCarts()
    {
        if (CartItems.Count == 0)
        {
            Console.WriteLine("No carts available.");
            return;
        }
    
        Console.WriteLine("===== ALL SHOPPING CARTS =====");
        Console.WriteLine();
    
        foreach (var cartEntry in CartItems)
        {
            int cartId = cartEntry.Key;
            ShoppingCartService cart = cartEntry.Value;
        
            Console.WriteLine($"CART #{cartId}" + (cartId == ActiveCartID ? " (ACTIVE)" : ""));
            Console.WriteLine("----------------------------------------");
        
            if (cart.Items.Count == 0)
            {
                Console.WriteLine("This cart is empty.");
            }
            else
            {
                foreach (var item in cart.Items)
                {
                    Console.WriteLine($"{item.Product?.Name} - ${item.Product?.Price:F2} x {item.Quantity} = ${item.Subtotal:F2}");
                }
            
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"Subtotal: ${cart.Subtotal:F2}");
                Console.WriteLine($"Tax: ${cart.Tax:F2} ({cart.TaxRate * 100:F2}%)");
                Console.WriteLine($"Total: ${cart.Total:F2}");
            }
        
            Console.WriteLine();
            Console.WriteLine();
        }
    
        Console.WriteLine("===============================");
    }
    
    
}