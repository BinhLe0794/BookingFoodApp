using ApplicationServices.Models.Enums;

namespace ApplicationServices.Entities.Interfaces;

public static class SeedData
{
    public static List<Dish> SeedDishes()
    {
        var dishes = new List<Dish>();
        dishes.AddRange(Drinks());
        dishes.AddRange(Burgers());
        dishes.AddRange(Breads());
        dishes.AddRange(Sandwiches());
        dishes.AddRange(Pizzas());
        return dishes;
    }

    private static List<Dish> Drinks()
    {
        var price = new Random();
        var drinks = new List<Dish>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Drinks,
                Name = "Pepsi",
                Description = "The soft drink with more engery",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629186235045-80d4147d90dc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8N3x8cGVwc2l8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Drinks,
                Name = "CocaCola",
                Description = "The soft drink with more engery",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1592892111425-15e04305f961?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=928&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Drinks,
                Name = "Red Bull",
                Description =
                    "Red Bull includes carbonated beverages, fruit and vegetable juices, bottled water",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1613218222876-954978a4404e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2070&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Drinks,
                Name = "Red wine",
                Description =
                    "Enjoying a nice glass of bubbly",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1547595628-c61a29f496f0?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Drinks,
                Name = "Fresh Beer",
                Description =
                    "Beer is an alcoholic beverage produced by extracting raw materials with water",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1618183479302-1e0aa382c36b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Drinks,
                Name = "Jarritos",
                Description =
                    "MXCN Cola is an alcoholic beverage produced by extracting coca with lemon",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1618183479302-1e0aa382c36b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            }
        };
        return drinks;
    }

    private static List<Dish> Burgers()
    {
        var price = new Random();
        var burgers = new List<Dish>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Beef Burger",
                Description =
                    "A combination of Beef, Cheese and others seasoning ingredients",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1550547660-d9450f859349?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1065&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Tower Cheese",
                Description =
                    "Burger with many special ingredients in large quantities",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1553979459-d2229ba7433b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1968&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Four Seasons Country",
                Description =
                    "A masterpiece from afar in the west of the middle east",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1632577237955-f73cb2a054ec?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1001&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Potato Tomato Burger",
                Description =
                    "",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1560130803-aaadb4bc913e?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Blue Sea Salt Burger",
                Description =
                    "The ingredients are selected from the remote and fresh eastern sea",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1596662977545-485cfa84f149?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Mutton Burger",
                Description =
                    "Meatballs are made from fresh lamb from big farms",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1525164286253-04e68b9d94c6?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            }
        };
        return burgers;
    }

    private static List<Dish> Breads()
    {
        var price = new Random();
        var breads = new List<Dish>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Premium Fresh Avocado Bread",
                Description =
                    "Made from barley grains that are nutritious and good for health",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1545288907-ffa8bdb07477?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1064&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Old Honey Barn Breads",
                Description =
                    "Made from raw rice grains, keeping essential nutrients intact",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1507638940746-7b17d6b55b8f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1016&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Special Set 1",
                Description =
                    "Set of food includes bread, ice cream, fruit and nuts",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1570690050322-26d6a2ec7f3a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1974&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Special Set 2",
                Description =
                    "Food set includes bread, banana, pure honey",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1567620905732-2d1ec7ab7445?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1080&q=80"
            }
        };
        return breads;
    }

    private static List<Dish> Sandwiches()
    {
        var price = new Random();
        var sandwiches = new List<Dish>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Ostrich Egg Sandwiches",
                Description =
                    "Ostrich Egg without Spices and Fresh Vegetables",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1621188988909-fbef0a88dc04?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Special Set 3",
                Description =
                    "Special meal for 2 people including bread, bell pepper, lemon tea and french fries",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2070&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Lover's Tomato Bread",
                Description =
                    "The freshness of organic unprocessed tomatoes",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1620991635798-fed0b0f149c8?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Red Omion Sandwiches",
                Description =
                    "Hybrid bread with burger and red onion",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1481070555726-e2fe8357725c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1035&q=80"
            }
        };
        return sandwiches;
    }

    private static List<Dish> Pizzas()
    {
        var price = new Random();
        var pizzas = new List<Dish>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Pizzas,
                Name = "Potato Seafood Pizzas",
                Description =
                    "Fresh seafood pizzas with sea salt mashed potatoes",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1597715469889-dd75fe4a1765?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=987&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Pizzas,
                Name = "Special Set 4",
                Description =
                    "Large Pizzas for a family of 4-6 people",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1600628421066-f6bda6a7b976?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2070&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Pizzas,
                Name = "Vegetable Pizzas",
                Description =
                    "Vegetarian veggie pizzas are healthy and low in calories",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1585238342024-78d387f4a707?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1480&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Pizzas,
                Name = "Special Set 5",
                Description =
                    "Enjoy comfort and satiation with friends and relatives",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1594179047519-f347310d3322?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2070&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Pizzas,
                Name = "Pizzas Bursting with Flavors",
                Description =
                    "Pizzas’s own paprika and cucumber mayonnaise add the crowning touch.",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1628672092908-c92a710bb3d6?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1035&q=80"
            }
        };
        return pizzas;
    }
}