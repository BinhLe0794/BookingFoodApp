using System.ComponentModel.DataAnnotations;

namespace ApplicationServices.Requests;

public class UserPasswordChangeRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string UserId { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string NewPassword { get; set; }
}