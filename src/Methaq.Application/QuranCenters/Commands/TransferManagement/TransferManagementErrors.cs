using ErrorOr;

namespace Methaq.Application.QuranCenters.Commands.TransferManagement;

public static class TransferManagementErrors
{
    public static readonly Error CenterNotFound = Error.NotFound(
        code: "QuranCenter.NotFound",
        description: "Center not found.");

    public static readonly Error NewManagerNotFound = Error.NotFound(
        code: "QuranCenter.NewManagerNotFound",
        description: "New manager not found.");

    public static readonly Error OldManagerNotFound = Error.NotFound(
    code: "QuranCenter.OldManagerNotFound",
    description: "Current manager not found.");

    public static readonly Error TransferFailed = Error.Failure(
        code: "QuranCenter.TransferFailed",
        description: "Failed to transfer management. Please try again.");
}