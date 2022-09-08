using System.Diagnostics;
using ApplicationServices.Config;
using ApplicationServices.Entities;
using ApplicationServices.Entities.Interfaces;
using ApplicationServices.Models.Accounts;
using ApplicationServices.Models.Menu;
using ApplicationServices.Requests;
using FoodAppServer.Extensions;
using FoodAppServer.Models;
using FoodAppServer.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodAppServer.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly UserManager<Account> _userManager;

    public HomeController(ApplicationDbContext context, UserManager<Account> userManager,
        SignInManager<Account> signInManager,
        RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
    }

    public string basePath => _environment.WebRootPath;

    public IActionResult Index()
    {
        // "https://images.unsplash.com/photo-1509440159596-0249088772ff?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1744&q=80",
        // "https://images.unsplash.com/photo-1639129224805-7258e3d52840?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=928&q=80",
        // "https://images.unsplash.com/photo-1551538827-9c037cb4f32a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=930&q=80",
        // "https://images.unsplash.com/photo-1481070414801-51fd732d7184?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1448&q=80",
        // "https://images.unsplash.com/photo-1520201163981-8cc95007dd2a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTJ8fHBpenphc3xlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60"
        var categoryEnums = Helper.GetCategory();
        var categories = new List<CategoryVm>();
        foreach (var cate in categoryEnums)
        {
            var addCategory = new CategoryVm
            {
                Name = cate.ToString(),
                Image = Helper.GetImageCategory(cate)
            };
            categories.Add(addCategory);
        }

        return View(categories);
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    #region Authentication

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login([FromQuery] string ReturnUrl = "/")
    {
        return View(new LoginRequest {ReturnUrl = ReturnUrl});
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
        if (result.Succeeded) return Redirect(request.ReturnUrl);

        ModelState.AddModelError("", "Invalid UserName or Password");
        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> CurrentProfile()
    {
        var currentUser = User.Identity?.Name;
        if (string.IsNullOrEmpty(currentUser)) return RedirectToAction("Index");

        var user = await _userManager.FindByNameAsync(currentUser);
        var profileUser = new AccountVm
        {
            Id = user.Id,
            Fullname = user.Fullname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = Helper.GetAvatar(user.Id, basePath, string.Empty),
            LastLogin = user.LastLogin.HasValue ? user.LastLogin.Value.ToString() : "",
            Token = string.Empty,
            RefreshToken = string.Empty
        };
        return View(profileUser);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        var user = await _userManager.FindByNameAsync(request.Username);
        if (user != null)
        {
            ModelState.AddModelError("", "Username already have");
            return View(request);
        }

        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            ModelState.AddModelError("", "Email already have");
            return View(request);
        }

        var newAccount = new Account
        {
            Email = request.Email,
            UserName = request.Username,
            PhoneNumber = request.PhoneNumber,
            Fullname = request.Username,
            LastLogin = DateTime.Now
        };

        var result = await _userManager.CreateAsync(newAccount, request.Password);
        if (result.Succeeded) return RedirectToAction("Login", new LoginRequest {ReturnUrl = "/"});

        ModelState.AddModelError("", result.Errors.FirstOrDefault()?.Description ?? "Register Failed");
        return View(request);
    }

    #endregion

    #region AccountUser

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.Select(user => new AccountVm
        {
            Id = user.Id,
            Fullname = user.Fullname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = Helper.GetAvatar(user.Id, basePath, string.Empty),
            LastLogin = user.LastLogin.HasValue ? user.LastLogin.Value.ToString() : "",
            Token = string.Empty,
            RefreshToken = string.Empty
        }).ToListAsync();
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> UserDetail(string userId)
    {
        if (string.IsNullOrEmpty(userId)) return RedirectToAction("Index");

        var user = await _userManager.FindByIdAsync(userId);
        var profileUser = new AccountVm
        {
            Id = user.Id,
            Fullname = user.Fullname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Avatar = Helper.GetAvatar(user.Id, basePath, string.Empty),
            LastLogin = user.LastLogin.HasValue ? user.LastLogin.Value.ToString() : "",
            Token = string.Empty,
            RefreshToken = string.Empty
        };
        return View(profileUser);
    }

    [HttpGet]
    public IActionResult ForgetPassword(string userId)
    {
        var request = new UserPasswordChangeRequest
        {
            UserId = userId
        };
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> ForgetPassword(UserPasswordChangeRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return View(request);

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (result.Succeeded) return RedirectToAction("Login");

        ModelState.AddModelError("", result.Errors.FirstOrDefault()?.Description ?? "Change Failed");
        return View(request);
    }

    #endregion

    #region DishController

    [HttpGet]
    public async Task<IActionResult> GetDishes()
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
        }).ToListAsync();
        return View(dishes);
    }

    [HttpGet("Home/GetDishesbyCategory/{category}")]
    public async Task<IActionResult> GetDishesbyCategory(string category)
    {
        if (string.IsNullOrEmpty(category)) return BadRequest("Category is invalid");

        var key = Helper.GetCategoriesEnums(category);
        var dishes = await _context.Dishes.Where(x => x.Category == key).Select(
            x => new DishVm
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Calories = x.Calorie,
                Category = x.Category.ToString(),
                Description = x.Description,
                Image = x.ImageUrl,
                Price = x.Price,
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt
            }).ToListAsync();
        return View(dishes);
    }

    [HttpGet]
    public async Task<IActionResult> SeedingDish()
    {
        if (_context.Dishes.Any()) return RedirectToAction("GetDishes");
        await _context.Dishes.AddRangeAsync(SeedData.SeedDishes());
        await _context.SaveChangesAsync();
        return RedirectToAction("GetDishes");
    }

    [HttpGet("/Home/CreateDish")]
    public IActionResult CreateDish()
    {
        var request = new CreateDishRequest();
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDish(CreateDishRequest request)
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

    [HttpGet("/home/editDish/{dishID}")]
    public async Task<IActionResult> EditDish(Guid dishId)
    {
        if (string.IsNullOrEmpty(dishId.ToString())) return RedirectToAction("GetDishes");

        var dish = await _context.Dishes.FindAsync(dishId);
        if (dish == null) return NotFound();

        var request = new EditDishRequest
        {
            Id = dish.Id,
            Calorie = dish.Calorie,
            Category = dish.Category,
            Description = dish.Description,
            ImageUrl = dish.ImageUrl,
            Name = dish.Name,
            Price = dish.Price
        };
        return View(request);
    }

    [HttpPost]
    public async Task<IActionResult> EditDish(EditDishRequest request)
    {
        if (!ModelState.IsValid) return View(request);

        var dish = await _context.Dishes.FindAsync(request.Id);
        if (dish == null) return NotFound();

        dish.Name = request.Name;
        dish.Calorie = request.Calorie;
        dish.Description = request.Description;
        dish.ImageUrl = request.ImageUrl;
        dish.Category = request.Category;
        dish.Price = request.Price;

        _context.Dishes.Update(dish);
        var result = await _context.SaveChangesAsync();
        if (result > 0) return RedirectToAction("GetDishes");
        ModelState.AddModelError("", "Update Failed");
        return View(request);
    }

    #endregion

    #region OrderController

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders
            .Include(x => x.OrderDetails)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new OrderViewModel
            {
                Id = x.Id.ToString(),
                Account = new AccountVm
                {
                    Id = x.Account.Id,
                    Fullname = x.Account.Fullname,
                    Email = x.Account.Email,
                    PhoneNumber = x.Account.PhoneNumber,
                    Token = string.Empty,
                    RefreshToken = string.Empty
                },
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt
            }).ToListAsync();
        //QUERY ORDER DETAILS
        foreach (var order in orders)
        {
            var orderDetail = await _context.OrderDetails.Where(x => x.OrderId.ToString() == order.Id)
                .Include(x => x.Dish)
                .Select(x =>
                    new OrderDetailVm
                    {
                        Id = x.Id.ToString(),
                        Category = x.Dish.Category.ToString(),
                        ImageUrl = x.Dish.ImageUrl,
                        Name = x.Dish.Name,
                        Price = x.Dish.Price,
                        Quantity = x.Quantity
                    }).ToListAsync();
            order.OrderDetails.AddRange(orderDetail);
        }

        return View(orders);
    }

    [HttpGet("/home/GetOrdersbyUserId/{userId}")]
    public async Task<IActionResult> GetOrdersbyUserId(string userId)
    {
        var orders = await _context.Orders
            .Include(x => x.OrderDetails)
            .Where(x => x.AccountId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new OrderViewModel
            {
                Id = x.Id.ToString(),
                Account = new AccountVm
                {
                    Id = x.Account.Id,
                    Fullname = x.Account.Fullname,
                    Email = x.Account.Email,
                    PhoneNumber = x.Account.PhoneNumber,
                    Token = string.Empty,
                    RefreshToken = string.Empty
                },
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt
            }).ToListAsync();
        //QUERY ORDER DETAILS
        foreach (var order in orders)
        {
            var orderDetail = await _context.OrderDetails.Where(x => x.OrderId.ToString() == order.Id)
                .Include(x => x.Dish)
                .Select(x =>
                    new OrderDetailVm
                    {
                        Id = x.Id.ToString(),
                        Category = x.Dish.Category.ToString(),
                        ImageUrl = x.Dish.ImageUrl,
                        Name = x.Dish.Name,
                        Price = x.Dish.Price,
                        Quantity = x.Quantity
                    }).ToListAsync();
            order.OrderDetails.AddRange(orderDetail);
        }

        return View(orders);
    }

    [HttpGet("/Home/GetOrders/{orderId}")]
    public async Task<IActionResult> GetOrderDetails(string orderId)
    {
        var orderDetails = await _context.OrderDetails.Where(x => x.OrderId.ToString() == orderId)
            .Include(x => x.Dish)
            .Select(x =>
                new OrderDetailVm
                {
                    Id = x.Id.ToString(),
                    Category = x.Dish.Category.ToString(),
                    ImageUrl = x.Dish.ImageUrl,
                    Name = x.Dish.Name,
                    Price = x.Dish.Price,
                    Quantity = x.Quantity
                }).ToListAsync();
        return View(orderDetails);
    }
    #endregion
}