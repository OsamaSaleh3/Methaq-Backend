using ErrorOr;

namespace Methaq.Application.GroupChats.Commands.DeleteMessage;

public static class DeleteMessageErrors
{
    public static readonly Error MessageNotFound = Error.NotFound(
        code: "Message.NotFound",
        description: "Message not found.");

    public static readonly Error Unauthorized = Error.Forbidden(
        code: "Message.Unauthorized",
        description: "You are not allowed to delete this message.");
}