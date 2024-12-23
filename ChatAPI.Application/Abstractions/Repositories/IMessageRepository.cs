﻿using ChatAPI.Domain.Entities;

namespace ChatAPI.Application.Abstractions.Repositories;

public interface IMessageRepository
{
    public IEnumerable<Message> GetAllMessages();

    public Guid AddMessage(Message message);
    public Message? GetMessageById(Guid id);
}
