using System.ComponentModel.DataAnnotations;
using AdminApp.Models.Enums;

namespace AdminApp.Requests;

public class EditDishRequest
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required] public CategoryEnums Category { get; set; } = CategoryEnums.Common;

    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Description { get; set; } = string.Empty;

    public int Calorie { get; set; } = 0;

    public double Price { get; set; } = 0;

    public string ImageUrl { get; set; } = string.Empty;
}