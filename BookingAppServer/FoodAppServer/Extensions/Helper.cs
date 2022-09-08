using ApplicationServices.Models.Enums;

namespace FoodAppServer.Extensions;

public static class Helper
{
    public static string GetAvatar(string userId, string basePath, string domainName)
    {
        try
        {
            if (string.IsNullOrEmpty(userId)) return string.Empty;

            var pathToRead = Path.Combine(basePath, "Images");
            var photos = Directory
                .EnumerateFiles(pathToRead).FirstOrDefault(x => x.Contains(userId));
            if (!string.IsNullOrEmpty(photos)) return Path.Combine(domainName, "Images", Path.GetFileName(photos));

            return Path.Combine(domainName, "Images", Path.GetFileName("logo.png"));
        }
        catch
        {
            return string.Empty;
        }
    }

    public static IEnumerable<CategoryEnums> GetCategory()
    {
        var result = Enum.GetValues(typeof(CategoryEnums));
        return result.Cast<CategoryEnums>().ToList();
    }

    public static string GetImageCategory(CategoryEnums categoryEnums)
    {
        switch (categoryEnums)
        {
            case CategoryEnums.Common:
                return
                    "https://thumbs.dreamstime.com/z/common-everyday-food-product-vector-ilustration-227937648.jpg";
            case CategoryEnums.Burgers:
                return
                    "https://img.freepik.com/free-vector/cheese-burger-cartoon-illustration-flat-cartoon-style_138676-2875.jpg?w=740&t=st=1662016401~exp=1662017001~hmac=985351493d035a2e3f87e84515d314f3a1745913f1ac957b0e4bec386e61fc50";
            case CategoryEnums.Breads:
                return
                    "https://img.freepik.com/free-vector/four-pieces-toasts-with-jam_1308-71984.jpg?w=1380&t=st=1662016365~exp=1662016965~hmac=845aef7282da16edc9f35e924ec0706d6d0b51710be21560058fa6553e099cc0";
            case CategoryEnums.Sandwiches:
                return
                    "https://icons.iconarchive.com/icons/google/noto-emoji-food-drink/512/32395-green-salad-icon.png";
            case CategoryEnums.Drinks:
                return "https://vi.seaicons.com/wp-content/uploads/2017/03/drink-icon.png";
            case CategoryEnums.Pizzas:
                return "https://cdn.iconscout.com/icon/free/png-256/pizza-618-1114742.png";
            default:
                return
                    "https://previews.123rf.com/images/olgastrelnikova/olgastrelnikova1901/olgastrelnikova190100001/115903194-food-icon-with-smile-label-for-food-company-grocery-store-icon-vector-illustration-with-smiling-mout.jpg?fj=1";
        }
    }

    public static CategoryEnums GetCategoriesEnums(string category)
    {
        var lowerCategory = category.ToLower();
        switch (lowerCategory)
        {
            case "breads":
                return CategoryEnums.Breads;
            case "burgers":
                return CategoryEnums.Burgers;
            case "sandwiches":
                return CategoryEnums.Sandwiches;
            case "drinks":
                return CategoryEnums.Drinks;
            case "pizzas":
                return CategoryEnums.Pizzas;
            default:
                return CategoryEnums.Common;
        }
    }
}