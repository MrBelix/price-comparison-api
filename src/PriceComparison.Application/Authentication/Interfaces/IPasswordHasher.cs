namespace PriceComparison.Application.Authentication.Interfaces;

public interface IPasswordHasher
{
    /// <summary>
    /// Generate hash from password
    /// </summary>
    /// <param name="password">password to make a hash</param>
    /// <returns></returns>
    string HashPassword(string password);

    /// <summary>
    /// Compare password with hashed password
    /// </summary>
    /// <param name="hashedPassword">Hashed password</param>
    /// <param name="password">Password to compare with hash</param>
    /// <returns></returns>
    bool ValidatePassword(string hashedPassword, string password);
}