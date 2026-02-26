using ErrorOr;

namespace Methaq.Application.QuranCenters.Commands.UpdateCenterInfo;

public static class UpdateCenterInfoErrors
{
    public static readonly Error CenterNotFound = Error.NotFound(
        code: "QuranCenter.NotFound",
        description: "Center not found.");
}