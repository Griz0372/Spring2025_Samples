
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
        numberOfCarts = 1;
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
    
    
}