using ErrorOr;

namespace Methaq.Application.QuranCenters.Commands.RemoveSupervisor;

public static class RemoveSupervisorErrors
{
    public static readonly Error CenterNotFound = Error.NotFound(
        code: "QuranCenter.NotFound",
        description: "Center not found.");
}