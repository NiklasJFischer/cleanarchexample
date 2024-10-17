using ChatApi.Entities;
using System.Collections.Concurrent;

namespace ChatApi.Data
{
    public static class MessageStorage
    {
        public static readonly ConcurrentBag<Message> messages = [
            new Message() {
                Id = Guid.Parse("3fdb0bdf-fa84-44e3-b707-d0f91f01393f"),
                AuthorId = Guid.Parse("280e983f-612e-497c-9429-2ce00df84530"),
                Created = DateTime.UtcNow,
                Text = "A dummy text message."
            },
            new Message() {
                Id = Guid.Parse("728cab42-b414-457b-ad85-77fefec22f8a"),
                AuthorId = Guid.Parse("e18548cf-ce4d-4c84-b8c3-334f1eecfb19"),
                Created = DateTime.UtcNow,
                Text = "One more dummy text message." },
        ];
    }
}
