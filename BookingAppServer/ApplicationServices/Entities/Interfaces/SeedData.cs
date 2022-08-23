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
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Tucked in between three soft buns are two all-beef patties, cheddar cheese, ketchup, onion, pickles and iceberg lettuce.",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1551538827-9c037cb4f32a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=765&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Drinks,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629186235045-80d4147d90dc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8N3x8cGVwc2l8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60"
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
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1609167830240-fc81e9cfd9bf?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1609167830240-fc81e9cfd9bf?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1609167830240-fc81e9cfd9bf?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1609167830240-fc81e9cfd9bf?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1609167830240-fc81e9cfd9bf?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Burgers,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1609167830240-fc81e9cfd9bf?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
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
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1596662841962-34034e1e6efc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1596662841962-34034e1e6efc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1596662841962-34034e1e6efc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1596662841962-34034e1e6efc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1596662841962-34034e1e6efc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1596662841962-34034e1e6efc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Breads,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1596662841962-34034e1e6efc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"
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
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1629236055174-3e21cae6fa97?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
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
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Category = CategoryEnums.Sandwiches,
                Name = "Old Honey Barn Mint Julep",
                Description =
                    "Hesburger’s own paprika and cucumber mayonnaise add the crowning touch. Oh baby!",
                Calorie = price.Next(20, 70) + price.Next(10, 50),
                Price = price.Next(10, 20),
                ImageUrl =
                    "https://images.unsplash.com/photo-1513104890138-7c749659a591?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1740&q=80"
            }
        };
        return pizzas;
    }
}