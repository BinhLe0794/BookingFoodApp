using System.ComponentModel.DataAnnotations;

namespace AdminApp.Requests;

public class RegisterRequest : LoginRequest
{
    [Required(ErrorMessage = "E-mail can't be empty")]
    [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
    public string Email { get; set; }
    
    
    [Required(ErrorMessage = "Re-Type the Password")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    
    public string PhoneNumber { get; set; } = string.Empty;
    
}