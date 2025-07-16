namespace CafeteriaOrderingSystem.Models;

public class Employee
{
    public int Id { get; set; }
    public string EmployeeNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public DateTime LastDepositMonth { get; set; } = DateTime.Now;

    // Navigation properties
    public List<Order> Orders { get; set; } = new();
}
