using ChatAPI.Application.Commands;
using ChatAPI.Application.Commands.Core;
using ChatAPI.Application.Services;
using ChatAPI.Test.UseCases.Stubs;

namespace ChatAPI.Test.UseCases
{
    public class CreateMessageTests
    {
        [Fact]
        public void Normal()
        {
            //stubs
            var messageRepository = new TestMessageRepository();
            var dateTimeProvider = new TestDateTimeProvider();
            var userRepository = new TestUserRepository();

            //data
            var userId = userRepository.Add(new Domain.Entities.User());
            var userContext = new UserContext();
            userContext.UserId = userId;
            dateTimeProvider.UtcNow = new DateTime(2022, 1, 2);
            CreateMessageCommand command = new CreateMessageCommand(userContext, "Testar nytt meddelande");

            //test
            CreateMessageService service = new CreateMessageService(messageRepository, dateTimeProvider, userRepository);
            var result = service.Execute(command);

            Assert.NotNull(result);
            Assert.Equal(CommandResultStatusCode.Success, result.StatusCode);
            Assert.Equal(command.Text, result?.Result?.Text);
            Assert.Equal(dateTimeProvider.UtcNow, result?.Result?.Created);
            Assert.True(messageRepository.Entities.Count(msg => msg.Text.Equals(command.Text)) == 1);
            Assert.True(messageRepository.Entities.Count(msg => msg.Created.Equals(dateTimeProvider.UtcNow)) == 1);
            Assert.True(messageRepository.Entities.Count(msg => msg.AuthorId.Equals(userContext.UserId)) == 1);

        }
    }
}