// Models/Order.cs
namespace CafeteriaOrderingSystem.Models;

public class Order(int id, int employeeId, DateTime orderDate, decimal totalAmount, string status)
{
    public int Id { get; set; } = id;
    public int EmployeeId { get; set; } = employeeId;
    public DateTime OrderDate { get; set; } = orderDate;
    public decimal TotalAmount { get; set; } = totalAmount;
    public string Status { get; set; } = status;

    // Navigation properties
    public Employee? Employee { get; set; }
    public List<OrderItem> OrderItems { get; } = new();
}
