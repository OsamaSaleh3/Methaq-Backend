using ErrorOr;

namespace Methaq.Application.Auth.Commands.RegisterEmployee;

public static class RegisterEmployeeErrors
{
    public static readonly Error EmailAlreadyExists = Error.Conflict(
        code: "Auth.EmailExists",
        description: "Email is already registered.");

    public static readonly Error RegisterFailed = Error.Failure(
        code: "Auth.RegisterFailed",
        description: "Failed to register the employee. Please try again.");
}