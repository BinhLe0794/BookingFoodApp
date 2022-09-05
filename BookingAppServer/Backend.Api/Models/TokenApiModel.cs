using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Api.Models;

public class TokenApiModel
{
    [Required(ErrorMessage = "Token is required")]
    public string AccessToken { get; set; } = string.Empty;

    [Required(ErrorMessage = "Refresh Token is required")]
    public string RefreshToken { get; set; } = string.Empty;
}