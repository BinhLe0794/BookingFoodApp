using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApplicationServices.Entities.Interfaces;
using ApplicationServices.Models.Enums;

namespace ApplicationServices.Entities;

[Table("Dishes")]
public class Dish : ITracking
{
    [Key] public Guid Id { get; set; }

    [Required] public CategoryEnums Category { get; set; } = CategoryEnums.Common;

    [MaxLength(500)] [Required] public string Name { get; set; } = string.Empty;

    [MaxLength(500)] [Required] public string Description { get; set; } = string.Empty;

    [DefaultValue(0)] public int Calorie { get; set; } = 0;

    [DefaultValue(0)] public double Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public ICollection<OrderDetail>? OrderDetails { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}