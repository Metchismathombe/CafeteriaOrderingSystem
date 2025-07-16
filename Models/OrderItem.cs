// Models/OrderItem.cs
namespace CafeteriaOrderingSystem.Models;

public class OrderItem(int id, int orderId, int menuItemId, int quantity, decimal unitPriceAtTimeOfOrder)
{
    public int Id { get; set; } = id;
    public int OrderId { get; set; } = orderId;
    public int MenuItemId { get; set; } = menuItemId;
    public int Quantity { get; set; } = quantity;
    public decimal UnitPriceAtTimeOfOrder { get; set; } = unitPriceAtTimeOfOrder;
    public Order? Order { get; set; }
    public MenuItem? MenuItem { get; set; }
}