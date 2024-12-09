namespace ChatAPI.Application.Abstractions.Providers;

public interface IHashProvider
{
    string GenerateHashByPasswordAndSalt(string password, string salt);
    void CreatePassword(string password, out string hash, out string salt);
}
