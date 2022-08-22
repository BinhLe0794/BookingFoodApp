using AdminApp.Models.Enums;

namespace AdminApp.Models.Menu;

public class OrderDetailVm
{
    public string Id { get; set; }

    public CategoryEnums Category { get; set; } = CategoryEnums.Common;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Calorie { get; set; } = 0;

    public double Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public double Total { get; set; }
}