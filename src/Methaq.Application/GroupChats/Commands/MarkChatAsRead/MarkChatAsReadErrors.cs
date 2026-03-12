using ErrorOr;

namespace Methaq.Application.GroupChats.Commands.MarkChatAsRead;

public static class MarkChatAsReadErrors
{
    public static readonly Error ChatNotFound = Error.NotFound(
        code: "GroupChat.NotFound",
        description: "Group chat not found.");

    public static readonly Error MessageNotFound = Error.NotFound(
        code: "GroupChat.MessageNotFound",
        description: "Message not found.");

    public static readonly Error MessageNotInChat = Error.Validation(
        code: "GroupChat.MessageNotInChat",
        description: "Message does not belong to this chat.");
}