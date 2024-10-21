namespace ChatAPI.Hashing.Abstractions
{
    public interface IHashProvider
    {
        string GenerateHashByPasswordAndSalt(string password, string salt);
        void CreatePassword(string password, out string hash, out string salt);
    }
}
