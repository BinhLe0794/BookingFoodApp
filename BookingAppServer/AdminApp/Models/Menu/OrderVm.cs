using AdminApp.Entities;
using AdminApp.Models.Accounts;

namespace AdminApp.Models.Menu;

public class OrderVm
{
    public Guid Id { get; set; }

    public List<OrderDetailVm> OrderDetails { get; set; }
    public string AccountId { get; set; }

    public AccountVm Account { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}