using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminApp.Entities.Interfaces;

namespace AdminApp.Entities;

[Table("Orders")]
public class Order : ITracking
{
    [Key] public Guid Id { get; set; }

    [Required] [ForeignKey("AccountId")] public string AccountId { get; set; }

    public Account Account { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}