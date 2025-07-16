// Controllers/OrderController.cs
using CafeteriaOrderingSystem.Data;
using CafeteriaOrderingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CafeteriaOrderingSystem.Controllers
{
    public class OrderController(ApplicationDbContext context, ILogger<OrderController> logger, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<OrderController> _logger = logger;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        [HttpGet]
        public async Task<IActionResult> Menu(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
                return NotFound("Restaurant not found.");

            return View(restaurant); // This looks for Views/Order/Menu.cshtml
        }


        [HttpGet]
        public async Task<IActionResult> Restaurants()
        {
            var restaurants = await _context.Restaurants.ToListAsync();
            return View(restaurants); // This looks for Views/Order/Restaurants.cshtml
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == user.UserName);
            if (employee == null) return NotFound("Employee not found");

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.EmployeeId == employee.Id)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders); // This looks for Views/Order/MyOrders.cshtml
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int restaurantId, Dictionary<int, int> quantities)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == user.UserName);
            if (employee == null) return NotFound("Employee not found");

            var restaurant = await _context.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == restaurantId);
            if (restaurant == null) return NotFound("Restaurant not found");

            if (quantities.Count == 0) return BadRequest("No items selected");

            decimal total = 0;
            var orderItems = new List<OrderItem>();

            foreach (var (menuItemId, quantity) in quantities)
            {
                if (!restaurant.MenuItems.Any(m => m.Id == menuItemId))
                    return BadRequest($"Menu item {menuItemId} not found");

                var menuItem = restaurant.MenuItems.First(m => m.Id == menuItemId);
                if (quantity <= 0) continue;

                total += menuItem.Price * quantity;
                orderItems.Add(new OrderItem(
                    id: 0,
                    orderId: 0,
                    menuItemId: menuItemId,
                    quantity: quantity,
                    unitPriceAtTimeOfOrder: menuItem.Price
                ));
            }

            if (total == 0) return BadRequest("No valid items selected");
            if (employee.Balance < total) return BadRequest("Insufficient balance");

            var order = new Order(
                id: 0,
                employeeId: employee.Id,
                orderDate: DateTime.UtcNow,
                totalAmount: total,
                status: "Pending"
            );

            try
            {
                employee.Balance -= total;
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var item in orderItems)
                {
                    item.OrderId = order.Id;
                    _context.OrderItems.Add(item);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("OrderConfirmation", new { id = order.Id });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error saving order");
                return StatusCode(500, "Error saving order. Please try again.");
            }
        }

        // Other action methods remain the same...
    }
}