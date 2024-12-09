using ChatAPI.Application.Abstractions.Providers;

namespace ChatAPI.Test.UseCases.Stubs
{
    public class TestDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow { get; set; } = DateTime.Now;


    }
}
