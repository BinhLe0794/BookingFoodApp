using ApplicationServices.Config;
using ApplicationServices.Entities;
using ApplicationServices.Entities.Interfaces;
using ApplicationServices.Models.Common;
using ApplicationServices.Models.Menu;
using ApplicationServices.Requests;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var dishes = await _context.Dishes.Select(x => new DishVm
            {
                Id = x.Id.ToString(),
                Category = x.Category.ToString(),
                Name = x.Name,
                Description = x.Description,
                Calories = x.Calorie,
                Price = x.Price,
                Image = x.ImageUrl,
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt
            })
            .ToListAsync();
        return View(dishes);
    }

    [Authorize]
    [HttpGet("/categories")]
    public IActionResult Categories()
    {
        // var categories = new List<string>()
        // {
        //     CategoryEnums.Breads.ToString(),
        //     CategoryEnums.Burgers.ToString(),
        //     CategoryEnums.Drinks.ToString(),
        //     CategoryEnums.Sandwiches.ToString(),
        //     CategoryEnums.Pizzas.ToString(),
        // }
        var categories = new List<string>
        {
            "https://images.unsplash.com/photo-1509440159596-0249088772ff?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1744&q=80",
            "https://images.unsplash.com/photo-1639129224805-7258e3d52840?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=928&q=80",
            "https://images.unsplash.com/photo-1551538827-9c037cb4f32a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=930&q=80",
            "https://images.unsplash.com/photo-1481070414801-51fd732d7184?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1448&q=80",
            "https://images.unsplash.com/photo-1520201163981-8cc95007dd2a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTJ8fHBpenphc3xlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60"
        };
        var result = new ApiSuccessResult<List<string>>(categories);
        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreateAsyncRange()
    {
        await _context.Dishes.AddRangeAsync(SeedData.SeedDishes());
        var result = await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDishRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        var dish = new Dish
        {
            Id = Guid.NewGuid(),
            Category = request.Category,
            Name = request.Name,
            Description = request.Description,
            Calorie = request.Calorie,
            Price = request.Price,
            ImageUrl = request.ImageUrl
        };
        await _context.Dishes.AddAsync(dish);
        var result = await _context.SaveChangesAsync();
        if (result > 0) return RedirectToAction("Index");
        ModelState.AddModelError("", "Create Failed");
        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        // if (string.IsNullOrEmpty(id))
        // {
        //     ModelState.AddModelError("", "Id is missing");
        //     return View();
        // }

        var dish = await _context.Dishes.FindAsync(id);
        if (dish == null)
        {
            ModelState.AddModelError("", "Id is missing");
            return View();
        }

        var request = new EditDishRequest
        {
            Id = dish.Id,
            Category = dish.Category,
            Name = dish.Name,
            Description = dish.Description,
            Calorie = dish.Calorie,
            Price = dish.Price,
            ImageUrl = dish.ImageUrl
        };
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditDishRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        var dish = await _context.Dishes.FindAsync(request.Id);
        if (dish == null)
        {
            ModelState.AddModelError("", "Id is missing");
            return View();
        }

        dish.Category = request.Category;
        dish.Name = request.Name;
        dish.Description = request.Description;
        dish.Calorie = request.Calorie;
        dish.Price = request.Price;
        dish.ImageUrl = request.ImageUrl;
        _context.Dishes.Update(dish);
        var result = await _context.SaveChangesAsync();
        if (result <= 0)
        {
            ModelState.AddModelError("", "Edit Failed");
            return View();
        }

        return RedirectToAction("Index");
    }
}