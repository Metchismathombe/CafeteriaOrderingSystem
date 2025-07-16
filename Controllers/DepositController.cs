// DepositController.cs
using CafeteriaOrderingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaOrderingSystem.Controllers
{
    public class DepositController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepositController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string employeeNumber, decimal depositAmount)
        {
            if (depositAmount <= 0)
            {
                ModelState.AddModelError("", "Deposit must be greater than 0.");
                return View();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.EmployeeNumber == employeeNumber);

            if (employee == null)
            {
                ModelState.AddModelError("", "Employee not found.");
                return View();
            }

            var currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDepositMonth = new DateTime(employee.LastDepositMonth.Year, employee.LastDepositMonth.Month, 1);

            if (lastDepositMonth != currentMonth)
            {
                employee.LastDepositMonth = currentMonth;
                employee.Balance = 0; // Reset monthly balance
            }

            int bonusTimes = (int)(depositAmount / 250);
            var bonus = bonusTimes * 500;

            employee.Balance += depositAmount + bonus;

            await _context.SaveChangesAsync();

            ViewBag.Message = $"Deposit successful. Bonus applied: R{bonus}";
            return View();
        }
    }
}
