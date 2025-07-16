// Models/MenuItem.cs
namespace CafeteriaOrderingSystem.Models;

public class MenuItem(int id, string name, string description, decimal price, int restaurantId, string imageUrl)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
    public int RestaurantId { get; set; } = restaurantId;
    public string ImageUrl { get; set; } = imageUrl;

    // Navigation properties
    public Restaurant? Restaurant { get; set; }
    public List<OrderItem> OrderItems { get; } = new();
}
