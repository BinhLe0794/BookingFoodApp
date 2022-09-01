using ApplicationServices.Models.Menu;

namespace Backend.Api.Models;

public class HomePageVm
{
    public List<CategoryVm> Categories { get; set; } = new();

    public List<DishVm> Populars { get; set; } = new();

    public List<DishVm> Specials { get; set; } = new();
}