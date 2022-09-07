using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApplicationServices.Entities;
using ApplicationServices.Models.Accounts;
using ApplicationServices.Models.Common;
using ApplicationServices.Requests;
using FoodAppServer.Extensions;
using FoodAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FoodAppServer.Controllers;

public class AccountController : AuthAPIController
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
    [AllowAnonymous]
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

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var token = GenerateAccessToken(claims);
            var refToken = GenerateRefreshToken();
            user.LastLogin = DateTime.Now;
            user.AccessToken = token;
            user.AccessTokenExpiryTime = DateTime.Now.AddDays(1);
            user.RefreshToken = refToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);
            var profileUser = new AccountVm
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Token = token,
                RefreshToken = refToken
            };
            var basePath = _environment.WebRootPath;
            profileUser.Avatar = GetAvatar(user.Id, basePath);
            var result = new ApiSuccessResult<AccountVm>(profileUser);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }

    [HttpPost("/refresh-token")]
    [ApiValidationFilter]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken(TokenApiModel request)
    {
        try
        {
            var accessToken = request.AccessToken;
            var refreshToken = request.RefreshToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity!.Name; //this is mapped to the Name claim by default
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return BadRequest(new ApiErrorResult<bool>($"Not Found {username}"));
            if (user.RefreshToken != refreshToken)
                return BadRequest(new ApiErrorResult<TokenApiModel>("RefreshToken is not match"));
            if (user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest(new ApiErrorResult<TokenApiModel>("RefreshToken has expired"));
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var newAccessToken = GenerateAccessToken(claims);
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);
            var newToken = new TokenApiModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
            return Ok(new ApiSuccessResult<TokenApiModel>(newToken));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }

    [ApiValidationFilter]
    [HttpPost("/logout")]
    public async Task<IActionResult> RevokeToken(TokenApiModel request)
    {
        try
        {
            var accessToken = request.AccessToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity!.Name; //this is mapped to the Name claim by default
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return BadRequest(new ApiErrorResult<bool>($"Not Found {username}"));
            user.RefreshToken = null;
            user.AccessToken = string.Empty;
            await _userManager.UpdateAsync(user);
            return BadRequest(new ApiSuccessResult<bool>($"Revoke {username}"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }

    #region Private function

    private static string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ABCDEF"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var createToken = new JwtSecurityToken("https://webapi.food.com.vn",
            "https://webapi.food.com.vn",
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(createToken);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private static ClaimsPrincipal GetPrincipalFromExpiredToken(string jwtToken)
    {
        IdentityModelEventSource.ShowPII = true;
        SecurityToken validatedToken;
        var validationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidIssuer = "https://webapi.food.com.vn",
            ValidAudience = "https://webapi.food.com.vn",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0123456789ABCDEF"))
        };
        var principal
            = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
        var jwtSecurityToken = validatedToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }

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

            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, "Images/");
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            var fName = userId + postedFileExtension;
            var uploadsAvatar = Path.Combine(_environment.WebRootPath, "Images/" + fName);

            await using var stream = new FileStream(uploadsAvatar, FileMode.Create);
            await file.CopyToAsync(stream);

            return fName;
        }
        catch
        {
            return null;
        }
    }

    private static string GetAvatar(string userId, string basePath)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
            {
                return string.Empty;
            }

            var pathToRead = Path.Combine(basePath, "Images");
            var photos = Directory
                .EnumerateFiles(pathToRead).FirstOrDefault(x => x.Contains(userId));
            return photos ?? string.Empty;
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }

    #endregion
}