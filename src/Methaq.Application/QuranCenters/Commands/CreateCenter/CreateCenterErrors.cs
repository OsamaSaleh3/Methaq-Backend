using ErrorOr;

namespace Methaq.Application.QuranCenters.Commands.CreateCenter;

public static class CreateCenterErrors
{
    public static readonly Error ManagerNotFound = Error.NotFound(
        code: "QuranCenter.ManagerNotFound",
        description: "Manager not found.");

    public static readonly Error ManagerNotActive = Error.Conflict(
        code: "QuranCenter.ManagerNotActive",
        description: "Employee is not active.");

    public static readonly Error ManagerNotEligible = Error.Conflict(
        code: "QuranCenter.ManagerNotEligible",
        description: "Employee is not a center manager.");

    public static readonly Error ManagerAlreadyAssignedToCenter = Error.Conflict(
        code: "QuranCenter.ManagerAlreadyAssigned",
        description: "Employee is already assigned to another center.");
}