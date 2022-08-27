using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApplicationServices.Entities;
using ApplicationServices.Models.Accounts;
using ApplicationServices.Models.Common;
using ApplicationServices.Requests;
using Backend.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly UserManager<Account> _userManager;
    private readonly IWebHostEnvironment _environment;

    public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager,
        RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _environment = environment;
    }

    public class RegisterFileRequest : RegisterRequest
    {
        public IFormFile? FileUpload { get; set; }
    }

    [HttpPost("/register")]
    [ApiValidationFilter]
    public async Task<IActionResult> Register([FromForm] RegisterFileRequest request)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user != null)
            {
                return BadRequest(new ApiErrorResult<AccountVm>("Already this user"));
            }

            user = await _userManager.FindByEmailAsync(request.Username);
            if (user != null)
            {
                return BadRequest(new ApiErrorResult<AccountVm>("Already this user"));
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
            if (!result.Succeeded)
            {
                return BadRequest(new ApiErrorResult<AccountVm>("Register Failed"));
            }

            if (request.FileUpload != null)
            {
                var newUser = await _userManager.FindByNameAsync(newAccount.UserName);
                if (newUser != null)
                {
                    await SaveAvatar(request.FileUpload, newUser.Id);
                }
            }

            return Ok(new ApiSuccessResult<AccountVm>(new AccountVm(newAccount)));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }

    [HttpPost("/upload-avatar")]
    [ApiValidationFilter]
    public async Task<IActionResult> UploadAvatar(IFormFile avatar, string fileName)
    {
        var filepath = await SaveAvatar(avatar, fileName);
        return Ok(filepath);
    }

    [AllowAnonymous]
    [HttpPost("/login")]
    [ApiValidationFilter]
    public async Task<IActionResult> Authentication(LoginRequest request)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return BadRequest(new ApiErrorResult<AccountVm>("Invaild Username or Password"));

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!signInResult.Succeeded)
            {
                return BadRequest(new ApiErrorResult<AccountVm>("Login Failed"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ABCDEF"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var createToken = new JwtSecurityToken("https://webapi.food.com.vn",
                "https://webapi.food.com.vn",
                // claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            var token = new JwtSecurityTokenHandler().WriteToken(createToken);
            var profileUser = new AccountVm
            {
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Token = token
            };
            profileUser.Avatar = GetAvatar(user.Id);
            var result = new ApiSuccessResult<AccountVm>(profileUser);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }
    [HttpGet("{userId}")]
    [ApiValidationFilter]
    public async Task<IActionResult> GetUserInfo(string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ApiErrorResult<AccountVm>("Check your request"));
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new ApiErrorResult<AccountVm>("Invalid User"));
            }
            var userVm = new AccountVm()
            {
                Fullname = user.Fullname,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Avatar = ""
            };
            return Ok(new ApiSuccessResult<AccountVm>(userVm));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }
    #region Private function
    private async Task<string?> SaveAvatar(IFormFile file, string userId)
    {
        try
        {
            var postedFileExtension = Path.GetExtension(file.FileName);
            if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var uploadsRootFolder = Path.Combine(_environment.ContentRootPath, "Images/");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            var fName = userId + postedFileExtension;
            var uploadsAvatar = Path.Combine(_environment.ContentRootPath, "Images/" + fName);

            await using var stream = new FileStream(uploadsAvatar, FileMode.Create);
            await file.CopyToAsync(stream);

            return fName;
        }
        catch
        {
            return null;
        }
    }

    private static string GetAvatar(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return string.Empty;
        }

        var pathToRead = Path.Combine(Directory.GetCurrentDirectory(), "Images");
        var photos = Directory
            .EnumerateFiles(pathToRead).FirstOrDefault(x => x.Contains(userId));
        return photos ?? string.Empty;
    }

    #endregion
}