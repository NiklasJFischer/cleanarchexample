using ChatAPI.Application.Abstractions.Providers;
using System.Security.Cryptography;

namespace ChatAPI.Hashing;

public class HashProvider : IHashProvider
{
    public string GenerateHashByPasswordAndSalt(string password, string salt)
    {
        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
        var saltBytes = Convert.FromBase64String(salt);
        byte[] passwordWithSaltBytes = [.. passwordBytes, .. saltBytes];
        string hash = Convert.ToBase64String(SHA256.HashData(passwordWithSaltBytes));
        return hash;
    }

    public void CreatePassword(string password, out string hash, out string salt)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(32);
        salt = Convert.ToBase64String(saltBytes);
        hash = GenerateHashByPasswordAndSalt(password, salt);
    }
}
