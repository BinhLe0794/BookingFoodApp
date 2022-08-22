using AdminApp.Config;
using AdminApp.Entities;
using AdminApp.Models.Menu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Controllers;

public class DishController : Controller
{
    public readonly ApplicationDbContext _context;
    // GET
    public DishController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dishes = await _context.Dishes.Select(x=> new DishVm()
            {
                Id = x.Id.ToString(),
                Category = x.Category,
                Name = x.Name,
                Description = x.Description,
                Calorie = x.Calorie,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt,
                Order = x.Order ?? new Order(),
            })
            .ToListAsync();
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit()
    {
        return View();
    }
}