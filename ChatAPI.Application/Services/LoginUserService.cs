using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Abstractions.Providers;
using ChatAPI.Application.Abstractions.Repositories;
using ChatAPI.Application.Commands;
using ChatAPI.Application.Commands.Core;
using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Services;

public class LoginUserService(IUserRepository userRepository, IHashProvider hashProvider, ITokenProvider tokenProvider) : UseCase<string>, ICommandService<LoginUserCommand, string>
{
    public CommandResult<string> Execute(LoginUserCommand command)
    {

        if (string.IsNullOrWhiteSpace(command?.Email))
        {
            return ValidationFailed("Email is null");
        }
        if (string.IsNullOrWhiteSpace(command?.Password))
        {
            return ValidationFailed("Password is null");
        }


        User? user = userRepository.GetUserByEmail(command.Email);

        if (user == null)
        {
            return ValidationFailed("Invalid email");
        }

        string hash = hashProvider.GenerateHashByPasswordAndSalt(command.Password, user.PasswordSalt);

        if (!user.PasswordHash.Equals(hash))
        {
            return ValidationFailed("Invalid password");
        }

        return Success(tokenProvider.CreateToken(user));
    }
}
