using ErrorOr;

namespace Methaq.Application.GroupChats.Commands.EditMessage;

public static class EditMessageErrors
{
    public static readonly Error MessageNotFound = Error.NotFound(
        code: "Message.NotFound",
        description: "Message not found.");

    public static readonly Error Unauthorized = Error.Forbidden(
        code: "Message.Unauthorized",
        description: "You are not allowed to edit this message.");
}