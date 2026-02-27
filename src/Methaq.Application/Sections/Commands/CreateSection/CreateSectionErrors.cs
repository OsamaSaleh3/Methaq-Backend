using ErrorOr;

namespace Methaq.Application.Sections.Commands.CreateSection;

public static class CreateSectionErrors
{
    public static readonly Error CenterNotFound = Error.NotFound(
        code: "Section.CenterNotFound",
        description: "Center not found.");

    public static readonly Error SupervisorNotFound = Error.NotFound(
        code: "Section.SupervisorNotFound",
        description: "Supervisor not found.");

    public static readonly Error SupervisorNotActive = Error.Conflict(
        code: "Section.SupervisorNotActive",
        description: "Supervisor is not active.");

    public static readonly Error SupervisorNotInCenter = Error.Conflict(
        code: "Section.SupervisorNotInCenter",
        description: "Supervisor does not belong to this center.");

    public static readonly Error CreateFailed = Error.Failure(
        code: "Section.CreateFailed",
        description: "Failed to create section. Please try again.");
}