using ErrorOr;

namespace Methaq.Application.QuranCenters.Commands.AddSupervisor;

public static class AddSupervisorErrors
{
    public static readonly Error CenterNotFound = Error.NotFound(
        code: "QuranCenter.NotFound",
        description: "Center not found.");

    public static readonly Error SupervisorNotFound = Error.NotFound(
        code: "QuranCenter.SupervisorNotFound",
        description: "Supervisor not found.");

    public static readonly Error SupervisorNotEmployee = Error.Conflict(
        code: "QuranCenter.SupervisorNotEmployee",
        description: "User is not an employee.");

    public static readonly Error SupervisorNotActive = Error.Conflict(
        code: "QuranCenter.SupervisorNotActive",
        description: "Supervisor is not active.");

    public static readonly Error SupervisorAlreadyAssigned = Error.Conflict(
    code: "QuranCenter.SupervisorAlreadyAssigned",
    description: "Supervisor is already assigned to another center.");
}