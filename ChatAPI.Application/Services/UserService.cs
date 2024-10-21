using ChatApi.ConsoleLogging;
using ChatApi.ConsoleLogging.Abstractions;
using ChatAPI.Application.Abstractions;
using ChatAPI.Application.Core;
using ChatAPI.DateTime;
using ChatAPI.DateTime.Abstractions;
using ChatAPI.Domain.Entities;
using ChatAPI.Domain.Enums;
using ChatAPI.Hashing;
using ChatAPI.Hashing.Abstractions;
using ChatAPI.InMemoryRepository;
using ChatAPI.InMemoryRepository.Abstractions;
using ChatAPI.Tokens;
using ChatAPI.Tokens.Abstractions;

namespace ChatAPI.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository = new UserRepository();
    private readonly IHashProvider hashProvider = new HashProvider();
    private readonly ITokenProvider tokenProvider = new TokenProvider();
    private readonly ILogRepository logRepository = new LogRepository();
    private readonly IConsoleLogger consoleLogger = new ConsoleLogger();
    private readonly IDateTimeProvider dateTimeProvider = new DateTimeProvider();

    public ServiceResult<string> LoginUser(UserContext userContext, string email, string password)
    {
        string jwtToken = "";
        StatusCode statusCode = StatusCode.Success;
        string resultMessage = "";

        try
        {

            if (string.IsNullOrWhiteSpace(email))
            {
                statusCode = StatusCode.ValidationFailed;
                resultMessage = "Email is null";
            }

            if (statusCode == StatusCode.Success && string.IsNullOrWhiteSpace(password))
            {
                statusCode = StatusCode.ValidationFailed;
                resultMessage = "Password is null";
            }


            if (statusCode == StatusCode.Success)
            {
                User? user = userRepository.GetUserByEmail(email);

                if (user == null)
                {
                    statusCode = StatusCode.ValidationFailed;
                    resultMessage = "Invalid email";
                }
                else
                {
                    string hash = hashProvider.GenerateHashByPasswordAndSalt(password, user.PasswordSalt);

                    if (!user.PasswordHash.Equals(hash))
                    {
                        statusCode = StatusCode.ValidationFailed;
                        resultMessage = "Invalid password";
                    }

                    jwtToken = tokenProvider.CreateToken(user);
                }
            }
        }
        catch (Exception ex)
        {
            var exLog = new Log() { Title = $"Login failed with unhandled error.", Description = ex.Message, Timestamp = dateTimeProvider.UtcNow };
            logRepository.AddLog(exLog);
            consoleLogger.AddLog(exLog);
            statusCode = StatusCode.Error;
        }

        var log = new Log() { Title = $"Command Login executed.", Description = $"Authenticated: {userContext.HasUserId}, StatusCode: {statusCode}", Timestamp = dateTimeProvider.UtcNow };
        logRepository.AddLog(log);
        consoleLogger.AddLog(log);

        return new ServiceResult<string>(jwtToken, statusCode, resultMessage);
    }
}
