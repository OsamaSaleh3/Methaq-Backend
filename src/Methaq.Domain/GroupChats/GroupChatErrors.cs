using ErrorOr;

namespace Methaq.Domain.GroupChats;

public static class GroupChatErrors
{
    public static readonly Error NameRequired = Error.Validation(
        code: "GroupChat.Name",
        description: "Group chat name is required.");

    public static readonly Error SectionIdRequired = Error.Validation(
        code: "GroupChat.SectionId",
        description: "Section ID is required.");

    public static readonly Error UserNull = Error.Validation(
        code: "GroupChat.UserNull",
        description: "User cannot be null.");

    public static readonly Error UserAlreadyMember = Error.Conflict(
        code: "GroupChat.AlreadyMember",
        description: "User is already a member of this group chat.");

    public static readonly Error MemberNotFound = Error.NotFound(
        code: "GroupChat.MemberNotFound",
        description: "Member not found in this group chat.");

    public static readonly Error MessageNull = Error.Validation(
        code: "GroupChat.MessageNull",
        description: "Message cannot be null.");

    public static readonly Error SenderNotMember = Error.Forbidden(
        code: "GroupChat.SenderNotMember",
        description: "Sender is not a member of this group chat.");

    public static readonly Error ChatIdRequired = Error.Validation(
        code: "GroupMessage.ChatId",
        description: "Group chat ID is required.");

    public static readonly Error SenderIdRequired = Error.Validation(
        code: "GroupMessage.SenderId",
        description: "Sender ID is required.");

    public static readonly Error ContentRequired = Error.Validation(
        code: "GroupMessage.Content",
        description: "Message content cannot be empty.");

    public static readonly Error CannotEditDeleted = Error.Conflict(
        code: "GroupMessage.Deleted",
        description: "Cannot edit a deleted message.");

    public static readonly Error CannotEditOthers = Error.Forbidden(
        code: "GroupMessage.EditForbidden",
        description: "Cannot edit another user's message.");

    public static readonly Error AlreadyDeleted = Error.Conflict(
        code: "GroupMessage.AlreadyDeleted",
        description: "Message is already deleted.");

    public static readonly Error CannotDeleteOthers = Error.Forbidden(
        code: "GroupMessage.DeleteForbidden",
        description: "Cannot delete another user's message.");
}
