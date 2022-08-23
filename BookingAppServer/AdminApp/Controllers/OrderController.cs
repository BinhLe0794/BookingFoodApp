using ApplicationServices.Config;
using ApplicationServices.Models.Accounts;
using ApplicationServices.Models.Menu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Controllers;

public class OrderController : Controller
{
    public readonly ApplicationDbContext _context;

    // GET
    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders.Include(x => x.Account)
            .Select(x => new OrderVm
            {
                Id = x.Id,
                AccountId = x.AccountId,
                Account = new AccountVm
                {
                    DisplayName = x.Account.UserName,
                    Email = x.Account.Email,
                    PhoneNumber = x.Account.PhoneNumber,
                    Fullname = x.Account.Fullname
                },
                OrderDetails = new List<OrderDetailVm>(),
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt
            }).ToListAsync();

        return View(orders);
    }
}