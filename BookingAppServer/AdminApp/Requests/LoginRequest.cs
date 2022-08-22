using System.ComponentModel.DataAnnotations;

namespace AdminApp.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    
    [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be between {2} and {1} characters")]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ReturnUrl { get; set; } = "/";
}