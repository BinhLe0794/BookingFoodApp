using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApplicationServices.Requests.Orders;

public class CheckoutOrderRequest
{
    [Required(ErrorMessage = "userId is required")]
    public string UserId { get; set; } = string.Empty;

    public List<OrderDetailRequest> Details { get; set; }

    public int Quantity => Details.Count;

    public double TotalCost => Details.Sum(detail => detail.Total);
}