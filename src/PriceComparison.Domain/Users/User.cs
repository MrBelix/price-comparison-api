using PriceComparison.Domain.Common;

namespace PriceComparison.Domain.Users;

public class User : Entity<UserId>
{
    public string Email { get; private set; }

    public string Password { get; private set; }

    private User(UserId id, string email, string password) : base(id)
    {
        Email = email;
        Password = password;
    }

    private User()
        : base(UserId.Empty)
    {
    }

    public static User Create(string email, string password)
    {
        var user = new User(UserId.New, email, password);

        return user;
    }
}