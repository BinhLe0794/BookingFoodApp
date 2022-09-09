using ApplicationServices.Config;
using ApplicationServices.Entities;
using ApplicationServices.Models.Common;
using ApplicationServices.Models.Menu;
using ApplicationServices.Requests.Orders;
using FoodAppServer.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodAppServer.Controllers;

public class OrderController : AuthAPIController
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("check-out")]
    [ApiValidationFilter]
    public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderRequest request)
    {
        try
        {
            if (!request.Details.Any()) return BadRequest(new ApiErrorResult<bool>("The Cart cannot be empty"));
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return BadRequest(new ApiErrorResult<bool>("The user is invalid"));
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                AccountId = request.UserId
            };
            await _context.Orders.AddAsync(order);
            var resultOrder = await _context.SaveChangesAsync();
            if (resultOrder <= 0) return BadRequest(new ApiErrorResult<bool>("Checkout failed"));

            var orderDetails = request.Details.Select(detail => new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    DishId = new Guid(detail.DishId),
                    OrderId = order.Id,
                    Quantity = detail.Quantity,
                    Total = detail.Total
                })
                .ToList();

            await _context.OrderDetails.AddRangeAsync(orderDetails);
            var result = await _context.SaveChangesAsync();
            if (result <= 0) return BadRequest(new ApiErrorResult<bool>("Checkout Failed"));

            return Ok(new ApiSuccessResult<bool>("Checkout Successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersByUser([FromQuery] string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest(new ApiErrorResult<bool>("user is invalid"));

            var orders = await _context.Orders
                .Where(x => x.AccountId == userId)
                .Include(x => x.OrderDetails)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new OrderVm
                {
                    Id = x.Id.ToString(),
                    OrderDetails = new List<OrderDetailVm>(),
                    CreatedAt = x.CreatedAt.ToString("yyy-MMM-dd hh:mm:ss")
                }).ToListAsync();
            //QUERY ORDER DETAILS
            foreach (var order in orders)
            {
                var orderDetail = await _context.OrderDetails.Where(x => x.OrderId.ToString() == order.Id)
                    .Include(x => x.Dish)
                    .Select(x =>
                        new OrderDetailVm
                        {
                            Id = x.Id.ToString(),
                            Category = x.Dish.Category.ToString(),
                            ImageUrl = x.Dish.ImageUrl,
                            Name = x.Dish.Name,
                            Price = x.Dish.Price,
                            Quantity = x.Quantity
                        }).ToListAsync();
                order.OrderDetails.AddRange(orderDetail);
            }

            return Ok(new ApiSuccessResult<List<OrderVm>>(orders));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderDetail(string orderId)
    {
        try
        {
            if (string.IsNullOrEmpty(orderId)) return BadRequest(new ApiErrorResult<bool>("Check your orderId"));

            var query = await _context.OrderDetails.Where(x => x.OrderId.ToString() == orderId).Include(x => x.Dish)
                .Select(x => new OrderDetailVm
                {
                    Id = x.Id.ToString(),
                    Category = x.Dish.Category.ToString(),
                    ImageUrl = x.Dish.ImageUrl,
                    Name = x.Dish.Name,
                    Price = x.Dish.Price,
                    Quantity = x.Quantity
                }).ToListAsync();
            return Ok(new ApiSuccessResult<List<OrderDetailVm>>(query));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiException<bool>(e));
        }
    }
}