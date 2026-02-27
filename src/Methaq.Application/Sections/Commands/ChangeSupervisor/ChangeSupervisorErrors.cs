using ErrorOr;

namespace Methaq.Application.Sections.Commands.ChangeSupervisor;

public static class ChangeSupervisorErrors
{
    public static readonly Error SectionNotFound = Error.NotFound(
        code: "Section.NotFound",
        description: "Section not found.");

    public static readonly Error SupervisorNotFound = Error.NotFound(
        code: "Section.SupervisorNotFound",
        description: "Supervisor not found.");

    public static readonly Error SupervisorNotActive = Error.Conflict(
        code: "Section.SupervisorNotActive",
        description: "Supervisor is not active.");

    public static readonly Error SupervisorNotInCenter = Error.Conflict(
        code: "Section.SupervisorNotInCenter",
        description: "Supervisor does not belong to this center.");
}