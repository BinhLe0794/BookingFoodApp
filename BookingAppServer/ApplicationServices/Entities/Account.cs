using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ApplicationServices.Entities;

[Table("Accounts")]
public class Account : IdentityUser
{
    [Required(ErrorMessage = "FullName is required")]
    [StringLength(255,
        ErrorMessage = "Must be between 3 and 255 characters",
        MinimumLength = 3)]
    public string Fullname { get; set; } = string.Empty;

    public string AccessToken { get; set; } = string.Empty;

    public DateTime? AccessTokenExpiryTime { get; set; }
    public string? RefreshToken { get; set; } = string.Empty;

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public DateTime? LastLogin { get; set; }

    public ICollection<Order> Orders { get; set; } = null!;
}