using ApplicationServices.Entities;

namespace ApplicationServices.Models.Accounts;

public class AccountVm
{
    public AccountVm()
    {
    }

    public AccountVm(Account account)
    {
        Fullname = account.Fullname;
        Email = account.Email;
        PhoneNumber = account.PhoneNumber;
        Token = string.Empty;
    }

    public string Fullname { get; set; }
    public string Email { get; set; }

    public string Avatar { get; set; } = string.Empty;
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}