using System.ComponentModel.DataAnnotations;

namespace FoodAppServer.Models;

public class TokenApiModel
{
    [Required(ErrorMessage = "Token is required")]
    public string AccessToken { get; set; } = string.Empty;

    [Required(ErrorMessage = "Refresh Token is required")]
    public string RefreshToken { get; set; } = string.Empty;
}