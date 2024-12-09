using ChatAPI.Domain.Entities;
using System.Collections.Concurrent;

namespace ChatAPI.InMemoryRepository.Data;

public static class LogStorage
{
    public static readonly ConcurrentBag<Log> logs = [];
}
