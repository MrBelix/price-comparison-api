using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using PriceComparison.Application.Authentication.Interfaces;

namespace PriceComparison.Infrastructure.Authentication;

/// <summary>
/// This password hasher is the same used by ASP.NET Identity.
/// Explanation: https://stackoverflow.com/questions/20621950/asp-net-identity-default-password-hasher-how-does-it-work-and-is-it-secure
/// Full implementation: https://gist.github.com/malkafly/e873228cb9515010bdbe
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        byte[] salt;
        byte[] passwordHashKey;
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException(nameof(password));
        }
        using (var key = new Rfc2898DeriveBytes(password, 0x10, 0x3e8, HashAlgorithmName.SHA512))
        {
            salt = key.Salt;
            passwordHashKey = key.GetBytes(0x20);
        }
        var storingPasswordArray = new byte[0x31];
        Buffer.BlockCopy(salt, 0, storingPasswordArray, 1, 0x10);
        Buffer.BlockCopy(passwordHashKey, 0, storingPasswordArray, 0x11, 0x20);
        return Convert.ToBase64String(storingPasswordArray);
    }

    public bool ValidatePassword(string hashedPassword, string password)
    {
        if (string.IsNullOrEmpty(hashedPassword))
            return false;
        if (password is null)
            throw new ArgumentNullException(password, "User Password should not be null !");

        var decodedStoredPasswordHash = Convert.FromBase64String(hashedPassword);

        if (IsInvalid(decodedStoredPasswordHash))
            return false;

        var salt = ExtractPortion(decodedStoredPasswordHash, 1, 0x10);
        var expectedPassword = ExtractPortion(decodedStoredPasswordHash,0x11, 0x20);

        var generatedKey = DeriveKeyFromPassword(password,salt);

        return ByteArraysEqual(expectedPassword, generatedKey);

    }

    private static bool IsInvalid(byte[] hashed)
    {
        return hashed.Length != 0x31 || hashed[0] != 0;
    }

    private static byte[] DeriveKeyFromPassword(string userInputData, byte[] salt)
    {
        using Rfc2898DeriveBytes key = new(userInputData,salt, 0x3e8, HashAlgorithmName.SHA512);
        return key.GetBytes(0x20);
    }

    private static byte[] ExtractPortion(byte[] source, int offset, int length)
    {
        var result = new byte[length];
        Buffer.BlockCopy(source, offset, result, 0, length);
        return result;
    }

    [MethodImpl(MethodImplOptions.NoOptimization)]
    private bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (ReferenceEquals(a, b))
            return true;

        if (a == null || b == null || a.Length != b.Length)
            return false;

        return a.SequenceEqual(b);
    }
}