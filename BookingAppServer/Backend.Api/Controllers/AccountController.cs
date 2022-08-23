using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApplicationServices.Entities;
using ApplicationServices.Models.Accounts;
using ApplicationServices.Models.Common;
using ApplicationServices.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<Account> _signInManager;
    private readonly UserManager<Account> _userManager;

    public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Authentication(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null) return BadRequest(new ApiErrorResult<AccountVm>("Invaild Username or Password"));

        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("issuer"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var createToken = new JwtSecurityToken("issuer",
            "audience",
            // claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);
        var token = new JwtSecurityTokenHandler().WriteToken(createToken);
        var profileUser = new AccountVm
        {
            Fullname = user.Fullname,
            DisplayName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Token = token
        };
        var result = new ApiSuccessResult<AccountVm>(profileUser);
        return Ok(result);
    }
}