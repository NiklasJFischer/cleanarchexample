using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions;

public interface ITokenProvider
{
    string CreateToken(User user);
    string Issuer { get; }
    string Audience { get; }
    string Key { get; }
}
