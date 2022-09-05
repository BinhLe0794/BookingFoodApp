using ApplicationServices.Models.Enums;

namespace ApplicationServices.Models.Menu;

public class OrderDetailVm
{
    public string Id { get; set; }

    public string Category { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public double Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public int Quantity { get; set; }
    public double Total => Math.Round(Quantity * Price, 2);
}