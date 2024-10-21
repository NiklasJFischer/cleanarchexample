using ChatAPI.Domain.Entities;

namespace ChatAPI.Tokens.Abstractions
{
    public interface ITokenProvider
    {
        string CreateToken(User user);
        string Issuer { get; }
        string Audience { get; }
        string Key { get; }
    }
}
