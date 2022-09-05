using ApplicationServices.Models.Accounts;

namespace ApplicationServices.Models.Menu;

public class OrderVm
{
    public string Id { get; set; }
    public List<OrderDetailVm> OrderDetails { get; set; }
    public int Quantity => OrderDetails.Count;
    public double TotalCost => OrderDetails.Sum(detail => detail.Total);
    public string CreatedAt { get; set; } = string.Empty;
}