using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminApp.Entities.Interfaces;

namespace AdminApp.Entities;

[Table("Orders")]
public class Order : ITracking
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [DefaultValue(1)]
    public int Quantity { get; set; }
    [Required]
    [DefaultValue(0)]
    public double Total { get; set; }

    public ICollection<Dish>? Dishes { get; set; } = null!;
    
    [Required]
    [ForeignKey("AccountId")]
    public string AccountId { get; set; }
    
    public Account Account { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}