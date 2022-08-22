using System.Security.Claims;
using AdminApp.Entities;
using AdminApp.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdminApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<Account> _userManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager,RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }
    //
    [HttpGet("/register")]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user != null)
        {
            ModelState.AddModelError("","Username already have");
            return View(request);
        }
        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            ModelState.AddModelError("","Email already have");
            return View(request);
        }

        var newAccount = new Account()
        {
            Email = request.Email,
            UserName = request.Username,
            PhoneNumber = request.PhoneNumber,
            Fullname = request.Username,
            LastLogin = DateTime.Now
        };
        var result = await _userManager.CreateAsync(newAccount, request.Password);
        if (result.Succeeded) return RedirectToAction("Authentication", "Account");
        
         ModelState.AddModelError("", result.Errors.FirstOrDefault()?.Description ?? "Register Failed");
        return View(request);
    }
    [HttpPost("/loadAvatar")]
    public IActionResult UpdateAvatar(IFormFile formFile)
    {
        
        return Ok();
    }
    //
    [HttpGet("/login")]
    public IActionResult Authentication([FromQuery]string ReturnUrl = "/")
    {
        
        return View(new LoginRequest(){ReturnUrl = ReturnUrl});
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Authentication(LoginRequest request)
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
        else
        {
            ModelState.AddModelError("", "Invalid UserName or Password");
            return View(request);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Authentication");
    }
    [HttpGet]
    public IActionResult ForgetPassword(string userId)
    {
        var request = new UserPasswordChangeRequest()
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
        if (result.Succeeded)
        {
            return RedirectToAction("Authentication");
        }
        ModelState.AddModelError("", result.Errors.FirstOrDefault()?.Description ?? "Change Failed");
        return View(request);
    }
}