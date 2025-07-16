// Data/SeedExtensions.cs
using CafeteriaOrderingSystem.Models;

namespace CafeteriaOrderingSystem.Data;

public static class SeedExtensions
{
    public static void Seed(this ApplicationDbContext context)
    {
        if (!context.Restaurants.Any())
        {
            var restaurant = new Restaurant(
                id: 1,
                name: "Main Cafeteria",
                locationDescription: "Building A",
                contactNumber: "555-1234",
                imageUrl: "pic1.jpg"
            );

            restaurant.MenuItems.Add(new MenuItem(
                id: 1,
                name: "Burger",
                description: "Classic beef burger",
                price: 5.99m,
                restaurantId: restaurant.Id,
                imageUrl: "burger.jpg"
            ));

            restaurant.MenuItems.Add(new MenuItem(
                id: 2,
                name: "Pizza",
                description: "Margherita pizza",
                price: 7.99m,
                restaurantId: restaurant.Id,
                imageUrl: "pizza.jpg"
            ));

            context.Restaurants.Add(restaurant);
            context.SaveChanges();
        }
    }
}
