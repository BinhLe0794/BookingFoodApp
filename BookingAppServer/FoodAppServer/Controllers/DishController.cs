using ApplicationServices.Config;
using ApplicationServices.Models.Common;
using ApplicationServices.Models.Menu;
using FoodAppServer.Extensions;
using FoodAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodAppServer.Controllers;

public class DishController : AuthAPIController
{
    private readonly ApplicationDbContext _context;

    public DishController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("categories")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var result = new HomePageVm();
            result.Categories = await _context.Dishes.GroupBy(x => x.Category).Select(x => new CategoryVm
            {
                Name = x.Key.ToString(),
                Image = Helper.GetImageCategory(x.Key)
            }).ToListAsync();
            var random = new Random();
            var maxDish = await _context.Dishes.CountAsync();
            result.Populars = await _context.Dishes.Where(x=>!x.Name.Contains("Special"))
                .Skip(random.Next(0,maxDish - 1))
                .Take(5)
                .Select(x => new DishVm
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Calories = x.Calorie,
                Category = x.Category.ToString(),
                Description = x.Description,
                Image = x.ImageUrl,
                Price = x.Price
            }).ToListAsync();
            result.Specials = await _context.Dishes.Where(x => x.Name.Contains("Special"))
                .OrderBy(x=>x.Name)
                .Select(x => new DishVm
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Calories = x.Calorie,
                Category = x.Category.ToString(),
                Description = x.Description,
                Image = x.ImageUrl,
                Price = x.Price
            }).ToListAsync();
            return Ok(new ApiSuccessResult<HomePageVm>(result));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetDishByCategories([FromQuery] string category)
    {
        try
        {
            if (string.IsNullOrEmpty(category)) return BadRequest(new ApiErrorResult<bool>("Category is invalid"));

            var key = Helper.GetCategoriesEnums(category);
            var query = await _context.Dishes.Where(x => x.Category == key).Select(
                x => new DishVm
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Calories = x.Calorie,
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Image = x.ImageUrl,
                    Price = x.Price
                }).ToListAsync();
            return Ok(new ApiSuccessResult<List<DishVm>>(query));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }

    [HttpGet("/api/dishes")]
    [AllowAnonymous]
    public async Task<IActionResult> GetDishes()
    {
        try
        {
            var query = await _context.Dishes.Select(
                x => new DishVm
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Calories = x.Calorie,
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Image = x.ImageUrl,
                    Price = x.Price
                }).ToListAsync();
            return Ok(new ApiSuccessResult<List<DishVm>>(query));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }
}