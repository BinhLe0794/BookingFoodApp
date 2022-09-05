using System.ComponentModel.DataAnnotations;

namespace ApplicationServices.Requests.Orders;

public class OrderDetailRequest
{
    [Required(ErrorMessage = "DishId is required")]
    public string DishId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public double Price { get; set; }

    public double Total => Math.Round(Quantity * Price, 2);
}