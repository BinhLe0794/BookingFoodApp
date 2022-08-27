using System.ComponentModel.DataAnnotations;

namespace ApplicationServices.Requests;

public class RegisterRequest : LoginRequest
{
    [Required(ErrorMessage = "E-mail can't be empty")]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; }


    [Required(ErrorMessage = "Re-Type the Password")]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;


}