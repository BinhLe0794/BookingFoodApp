using ApplicationServices.Config;
using ApplicationServices.Models.Common;
using ApplicationServices.Models.Enums;
using ApplicationServices.Models.Menu;
using FoodAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodAppServer.Controllers
{
    public class DishController : AuthAPIController
    {
        private readonly ApplicationDbContext _context;

        public DishController(ApplicationDbContext context)
        {
            _context = context;
        }

        private static string GetImageCategory(CategoryEnums categoryEnums)
        {
            switch (categoryEnums)
            {
                case CategoryEnums.Common:
                    return
                        "https://thumbs.dreamstime.com/z/common-everyday-food-product-vector-ilustration-227937648.jpg";
                case CategoryEnums.Burgers:
                    return
                        "https://img.freepik.com/free-vector/cheese-burger-cartoon-illustration-flat-cartoon-style_138676-2875.jpg?w=740&t=st=1662016401~exp=1662017001~hmac=985351493d035a2e3f87e84515d314f3a1745913f1ac957b0e4bec386e61fc50";
                case CategoryEnums.Breads:
                    return
                        "https://img.freepik.com/free-vector/four-pieces-toasts-with-jam_1308-71984.jpg?w=1380&t=st=1662016365~exp=1662016965~hmac=845aef7282da16edc9f35e924ec0706d6d0b51710be21560058fa6553e099cc0";
                case CategoryEnums.Sandwiches:
                    return
                        "https://icons.iconarchive.com/icons/google/noto-emoji-food-drink/512/32395-green-salad-icon.png";
                case CategoryEnums.Drinks:
                    return "https://vi.seaicons.com/wp-content/uploads/2017/03/drink-icon.png";
                case CategoryEnums.Pizzas:
                    return "https://cdn.iconscout.com/icon/free/png-256/pizza-618-1114742.png";
                default:
                    return
                        "https://previews.123rf.com/images/olgastrelnikova/olgastrelnikova1901/olgastrelnikova190100001/115903194-food-icon-with-smile-label-for-food-company-grocery-store-icon-vector-illustration-with-smiling-mout.jpg?fj=1";
            }
        }

        private static CategoryEnums GetCategoriesEnums(string category)
        {
            var lowerCategory = category.ToLower();
            switch (lowerCategory)
            {
                case "breads":
                    return CategoryEnums.Breads;
                case "burgers":
                    return CategoryEnums.Burgers;
                case "sandwiches":
                    return CategoryEnums.Sandwiches;
                case "drinks":
                    return CategoryEnums.Drinks;
                case "pizzas":
                    return CategoryEnums.Pizzas;
                default:
                    return CategoryEnums.Common;
            }
        }

        [HttpGet("categories")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var result = new HomePageVm();
                result.Categories = await _context.Dishes.GroupBy(x => x.Category).Select(x => new CategoryVm()
                {
                    Name = x.Key.ToString(),
                    Image = GetImageCategory(x.Key)
                }).ToListAsync();

                result.Populars = await _context.Dishes.Take(5).Select(x => new DishVm
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Calories = x.Calorie,
                    Category = x.Category.ToString(),
                    Description = x.Description,
                    Image = x.ImageUrl,
                    Price = x.Price
                }).ToListAsync();
                result.Specials = await _context.Dishes.Skip(5).Take(5).Select(x => new DishVm
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
                if (string.IsNullOrEmpty(category))
                {
                    return BadRequest(new ApiErrorResult<bool>("No categories"));
                }

                var key = GetCategoriesEnums(category);
                var query = await _context.Dishes.Where(x => x.Category == key).Select(
                    x => new DishVm()
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
        public async Task<IActionResult> GetDishByCategories()
        {
            try
            {
                var query = await _context.Dishes.Select(
                    x => new DishVm()
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
}