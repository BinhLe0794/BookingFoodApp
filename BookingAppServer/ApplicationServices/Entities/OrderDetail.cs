using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationServices.Entities;

[Table("OrderDetails")]
public class OrderDetail
{
    [Key] public Guid Id { get; set; }
    [Required] [DefaultValue(1)] public int Quantity { get; set; }
    [Required] [DefaultValue(0)] public double Total { get; set; }
    [Required] [ForeignKey("OrderId")] public Guid OrderId { get; set; }
    public Order Order { get; set; }
    [Required] [ForeignKey("DishId")] public Guid DishId { get; set; }
    public Dish Dish { get; set; }
}