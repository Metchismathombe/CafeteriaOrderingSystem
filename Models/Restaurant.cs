// Models/Restaurant.cs
using CafeteriaOrderingSystem.Models;

namespace CafeteriaOrderingSystem.Models;

public class Restaurant(int id, string name, string locationDescription, string contactNumber, string imageUrl = "")
{
    // EF Core requires a parameterless constructor
    private Restaurant() : this(0, "", "", "", "") { }

    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string LocationDescription { get; set; } = locationDescription;
    public string ContactNumber { get; set; } = contactNumber;
    public string ImageUrl { get; set; } = imageUrl;

    public ICollection<MenuItem> MenuItems { get; } = new List<MenuItem>();
}
