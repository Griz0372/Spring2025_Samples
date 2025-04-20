public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; } 
    public int StockQuantity { get; set; } 
    
    public string? Display
    {
        get
        {
            return $"{Id}. {Name} - ${Price:F2}";
        }
    }
    
    public Product()
    {
        Name = string.Empty;
        Price = 0.0m;
        StockQuantity = 0;
    }
    
    public override string ToString()
    {
        return Display ?? string.Empty;
    }
}