using ErrorOr;

namespace Methaq.Application.QuranCenters.Commands.RemoveSupervisor;

public static class RemoveSupervisorErrors
{
    public static readonly Error CenterNotFound = Error.NotFound(
        code: "QuranCenter.NotFound",
        description: "Center not found.");

    public static readonly Error SupervisorNotFound = Error.NotFound(
    code: "QuranCenter.SupervisorNotFound",
    description: "Supervisor not found.");
}