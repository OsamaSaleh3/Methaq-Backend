using ErrorOr;

namespace Methaq.Domain.Messages;

public static class MessageErrors
{
    public static readonly Error ConversationIdRequired = Error.Validation(
        code: "Message.ConversationId",
        description: "Conversation ID is required.");

    public static readonly Error SenderIdRequired = Error.Validation(
        code: "Message.SenderId",
        description: "Sender ID is required.");

    public static readonly Error ContentRequired = Error.Validation(
        code: "Message.Content",
        description: "Message content cannot be empty.");

    public static readonly Error AlreadyRead = Error.Conflict(
        code: "Message.AlreadyRead",
        description: "Message is already read.");

    public static readonly Error CannotEditDeletedMessage = Error.Conflict(
        code: "Message.Deleted",
        description: "Cannot edit a deleted message.");

    public static readonly Error CannotEditOthersMessage = Error.Forbidden(
        code: "Message.EditForbidden",
        description: "Cannot edit another user's message.");

    public static readonly Error AlreadyDeleted = Error.Conflict(
        code: "Message.AlreadyDeleted",
        description: "Message is already deleted.");

    public static readonly Error CannotDeleteOthersMessage = Error.Forbidden(
        code: "Message.DeleteForbidden",
        description: "Cannot delete another user's message.");
}