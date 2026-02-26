using ErrorOr;

namespace Methaq.Application.QuranCenters.Commands.CloseCenter;

public static class CloseCenterErrors
{
    public static readonly Error CenterNotFound = Error.NotFound(
        code: "QuranCenter.NotFound",
        description: "Center not found.");
}