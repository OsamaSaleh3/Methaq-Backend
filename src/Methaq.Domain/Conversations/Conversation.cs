using ErrorOr;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;
using Methaq.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Methaq.Domain.Conversations;

public class Conversation : BaseEntity
{
    private readonly List<ApplicationUser> _participants = [];
    public IReadOnlyCollection<ApplicationUser> Participants => _participants.AsReadOnly();

    private readonly List<Message> _messages = [];
    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    protected Conversation() { }

    private Conversation(ApplicationUser user1, ApplicationUser user2)
    {
        _participants.Add(user1);
        _participants.Add(user2);
    }

    public static ErrorOr<Conversation> Create(ApplicationUser user1, ApplicationUser user2)
    {
        if (user1 == null || user2 == null)
            return ConversationErrors.InvalidParticipants;

        if (user1.Id == user2.Id)
            return ConversationErrors.CannotMessageYourself;

        return new Conversation(user1, user2);
    }

    public ErrorOr<Success> AddMessage(Message message)
    {
        if (message == null)
            return ConversationErrors.MessageCannotBeNull;

        if (!_participants.Any(p => p.Id == message.SenderId))
            return ConversationErrors.SenderNotParticipant;

        _messages.Add(message);
        MarkAsUpdated();
        return Result.Success;
    }

    public int GetUnreadCount(string userId) =>
        _messages.Count(m => !m.IsRead && m.SenderId != userId && !m.IsDeleted);

    public bool HasParticipant(string userId) =>
        _participants.Any(p => p.Id == userId);

    public ApplicationUser? GetOtherParticipant(string userId) =>
        _participants.FirstOrDefault(p => p.Id != userId);
}