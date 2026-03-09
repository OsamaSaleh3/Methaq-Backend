using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.UpdateQualifications
{
    public class UpdateQualificationsErrors
    {
        public static readonly Error SupervisorNotFound = Error.NotFound(
      code: "Supervisor.SupervisorNotFound",
      description: "Supervisor not found.");

        public static readonly Error SupervisorNotActive = Error.Conflict(
            code: "Supervisor.SupervisorNotActive",
            description: "Supervisor is not active.");
    }
}
