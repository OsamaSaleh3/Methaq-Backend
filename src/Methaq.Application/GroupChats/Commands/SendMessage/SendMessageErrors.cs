using ErrorOr;

namespace Methaq.Application.GroupChats.Commands.SendMessage;

public static class SendMessageErrors
{
    public static readonly Error ChatNotFound = Error.NotFound(
        code: "GroupChat.NotFound",
        description: "Group chat not found.");

    public static readonly Error SenderNotFound = Error.NotFound(
        code: "GroupChat.SenderNotFound",
        description: "Sender not found.");

    public static readonly Error SenderNotMember = Error.Forbidden(
        code: "GroupChat.SenderNotMember",
        description: "Sender is not a member of this group chat.");
}