using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Resign
{
    public class ResignErrors
    {
        public static readonly Error SupervisorNotFound = Error.NotFound(
             code: "Supervisor.SupervisorNotFound",
             description: "Supervisor not found.");

        public static readonly Error SupervisorNotActive = Error.Conflict(
            code: "Supervisor.SupervisorNotActive",
            description: "Supervisor is not active.");

        public static readonly Error SupervisorAlreadyResign= Error.Conflict(
            code: "Supervisor.SupervisorAlreadyResign",
            description: "Supervisor is Already Resign.");

        public static readonly Error SupervisorHasSupervisedSections =Error.Conflict(
            code:"Supervisor.SupervisorHasSupervisedSections",
            description:"You cannot resign because you have section under your supervision.");

        public static readonly Error SupervisorIsCenterManager = Error.Conflict(
            code: "Supervisor.SupervisorIsCenterManager",
            description: "You cannot resign because you are the center manager.Please transfer the center's management to another employee before resigning.");

    }
}
