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

    public static readonly Error SupervisorHasSupervisedSections = Error.Conflict(
           code: "Supervisor.SupervisorHasSupervisedSections",
           description: "You cannot resign because you have section under your supervision.");

    public static readonly Error SupervisorIsCenterManager = Error.Conflict(
        code: "Supervisor.SupervisorIsCenterManager",
        description: "You cannot resign because you are the center manager.Please transfer the center's management to another employee before resigning.");
}