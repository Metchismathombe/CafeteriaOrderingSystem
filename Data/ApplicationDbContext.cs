// Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CafeteriaOrderingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace CafeteriaOrderingSystem.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<MenuItem> MenuItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Decimal precision
            builder.Entity<Employee>()
                .Property(e => e.Balance)
                .HasPrecision(18, 2);

            builder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(18, 2);

            builder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            builder.Entity<OrderItem>()
                .Property(oi => oi.UnitPriceAtTimeOfOrder)
                .HasPrecision(18, 2);

            // Relationships
            builder.Entity<Restaurant>()
                .HasMany(r => r.MenuItems)
                .WithOne(m => m.Restaurant!)
                .HasForeignKey(m => m.RestaurantId);
        }

        public void Seed()
        {
            if (!Restaurants.Any())
            {
                var mainCafeteria = new Restaurant(
                    id: 1,
                    name: "Main Cafeteria",
                    locationDescription: "Building A, Ground Floor",
                    contactNumber: "123-456-7890",
                    imageUrl: "/images/pic1.jpg"
                );

                var menuItems = new List<MenuItem>
                {
                    new(
                        id: 1,
                        name: "Burger",
                        description: "Classic beef burger",
                        price: 5.99m,
                        restaurantId: 1,
                        imageUrl: "/images/burger.jpg"
                    ),
                    new(
                        id: 2,
                        name: "Pizza",
                        description: "Margherita pizza",
                        price: 7.99m,
                        restaurantId: 1,
                        imageUrl: "/images/pizza.jpg"
                    )
                };

                Restaurants.Add(mainCafeteria);
                MenuItems.AddRange(menuItems);
            }

            if (!Employees.Any())
            {
                Employees.AddRange(new List<Employee>
                {
                    new Employee { Id = 1, Name = "Admin User", EmployeeNumber = "EMP001", Balance = 1000.00m },
                    new Employee { Id = 2, Name = "Regular User", EmployeeNumber = "EMP002", Balance = 500.00m }
                });
            }

            SaveChanges();
        }
    }
}

