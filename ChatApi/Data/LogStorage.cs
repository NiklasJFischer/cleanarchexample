using ChatApi.Entities;
using System.Collections.Concurrent;

namespace ChatApi.Data
{
    public static class LogStorage
    {
        public static readonly ConcurrentBag<Log> logs = [];
    }
}
