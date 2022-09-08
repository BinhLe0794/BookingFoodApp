using ApplicationServices.Entities.Interfaces;
using ApplicationServices.Models.Accounts;
using ApplicationServices.Models.Menu;

namespace FoodAppServer.Models.Admin;

public class OrderViewModel : ITracking
{
    public string Id { get; set; } = string.Empty;

    public AccountVm Account { get; set; } = new();
    public List<OrderDetailVm> OrderDetails { get; set; } = new();

    public int Quantity => OrderDetails.Count;

    public double TotalCost => OrderDetails.Sum(detail => detail.Total);

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}