﻿using ChatAPI.Domain.Entities;
using System.Collections.Concurrent;

namespace ChatAPI.InMemoryRepository.Data;

public class UserStorage
{
    public static readonly ConcurrentBag<User> users = [
        new User() {
            Id = Guid.Parse("280e983f-612e-497c-9429-2ce00df84530"),
            Email = "test.testsson@test.se",
            Name = "Test Testsson",
            PasswordHash = "N6u1vjZGm5AEhNhW4DFHZXvloh4vIgkRQT3zKPt3/7E=",
            PasswordSalt = "jorpdgYkd4poRhAn7BF06qBAHQuhFSAp7PBgVMQ7YyQ="
        },
        new User() {
            Id = Guid.Parse("e18548cf-ce4d-4c84-b8c3-334f1eecfb19"),
            Email = "fake.fakesson@test.se",
            Name = "Fake Fakesson",
            PasswordHash = "szUd7io1acLBRcXBy0xBCCa+Jv6u+BY5wNlu9sTenEE=",
            PasswordSalt = "2v6cJPURSsTd+Qnl2UPT2ealXfAcPMcEVwcpU4FDzHU="
        }
     ];

    internal static IEnumerable<Guid> GetAllUserIds()
    {
        return users.Select(u => u.Id);
    }
    internal static User GetUserByIdInternal(Guid id)
    {
        var matches = users.Where(u => u.Id.Equals(id));

        if (!matches.Any())
        {
            throw new ArgumentException($"User with id {id} does not exist", nameof(id));
        }

        return matches.First();
    }
}
