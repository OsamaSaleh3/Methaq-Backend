using ErrorOr;

namespace Methaq.Domain.QuranCenters;

public static class QuranCenterErrors
{
    public static readonly Error NameRequired = Error.Validation(
        code: "QuranCenter.Name",
        description: "Center name is required.");

    public static readonly Error LocationRequired = Error.Validation(
        code: "QuranCenter.Location",
        description: "Center location is required.");

    public static readonly Error ManagerIdRequired = Error.Validation(
        code: "QuranCenter.ManagerId",
        description: "Manager ID is required.");

    public static readonly Error SupervisorNull = Error.Validation(
        code: "QuranCenter.SupervisorNull",
        description: "Supervisor cannot be null.");

    public static readonly Error SupervisorAlreadyAssigned = Error.Conflict(
        code: "QuranCenter.SupervisorExists",
        description: "Supervisor is already assigned to this center.");

    public static readonly Error SupervisorNotActive = Error.Conflict(
        code: "QuranCenter.SupervisorNotActive",
        description: "Supervisor must be active to be assigned.");

    public static readonly Error SupervisorNotFound = Error.NotFound(
        code: "QuranCenter.SupervisorNotFound",
        description: "Supervisor not found in this center.");

    public static readonly Error SectionNull = Error.Validation(
        code: "QuranCenter.SectionNull",
        description: "Section cannot be null.");

    public static readonly Error CenterClosed = Error.Conflict(
        code: "QuranCenter.Closed",
        description: "Cannot modify a closed center.");

    public static readonly Error AlreadyClosed = Error.Conflict(
        code: "QuranCenter.AlreadyClosed",
        description: "Center is already closed.");

    public static readonly Error SameManager = Error.Validation(
        code: "QuranCenter.SameManager",
        description: "New manager must be different from current manager.");

    public static readonly Error NewManagerNotSupervisor = Error.Validation(
        code: "QuranCenter.NewManagerNotSupervisor",
        description: "New manager must be an existing supervisor in this center.");
}
