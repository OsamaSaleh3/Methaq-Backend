using ErrorOr;
using System.Runtime.Intrinsics.X86;

namespace Methaq.Domain.Conversations;

public static class ConversationErrors
{
    public static readonly Error InvalidParticipants = Error.Validation(
        code: "Conversation.InvalidParticipants",
        description: "Participants are invalid.");

    public static readonly Error CannotMessageYourself = Error.Validation(
        code: "Conversation.SameUser",
        description: "Cannot create a conversation with yourself.");

    public static readonly Error MessageCannotBeNull = Error.Validation(
        code: "Conversation.MessageNull",
        description: "Message cannot be null.");

    public static readonly Error SenderNotParticipant = Error.Validation(
        code: "Conversation.SenderNotParticipant",
        description: "Sender is not a participant in this conversation.");
}