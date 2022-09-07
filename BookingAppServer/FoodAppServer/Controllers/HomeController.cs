using System.Diagnostics;
using ApplicationServices.Entities;
using ApplicationServices.Models.Accounts;
using ApplicationServices.Requests;
using Microsoft.AspNetCore.Mvc;
using FoodAppServer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FoodAppServer.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly UserManager<Account> _userManager;
    private readonly IWebHostEnvironment _environment;

    public HomeController(UserManager<Account> userManager, SignInManager<Account> signInManager,
        RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _environment = environment;
    }

    public IActionResult Index()
    {
        return View();
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
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);
        if (result.Succeeded)
        {
            return Redirect(request.ReturnUrl);
        }

        ModelState.AddModelError("", "Invalid UserName or Password");
        return View(request);
    }

    [HttpGet]
    public async Task<IActionResult> CurrentProfile()
    {
        var currentUser = User.Identity?.Name;
        if (string.IsNullOrEmpty(currentUser))
        {
            return RedirectToAction("Index");
        }

        var user = await _userManager.FindByNameAsync(currentUser);
        var profileUser = new AccountVm
        {
            Id = user.Id,
            Fullname = user.Fullname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
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
        if (result.Succeeded) return RedirectToAction("Login", new LoginRequest() {ReturnUrl = "/"});

        ModelState.AddModelError("", result.Errors.FirstOrDefault()?.Description ?? "Register Failed");
        return View(request);
    }

    #endregion
}