using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Reactivate
{
    public class ReactivateErrors
    {
        public static readonly Error SupervisorNotFound = Error.NotFound(
             code: "Supervisor.SupervisorNotFound",
             description: "Supervisor not found.");


        public static readonly Error SupervisorAlreadyActive= Error.Conflict(
            code: "Supervisor.SupervisorAlreadyActive",
            description: "Supervisor is Already Active.");

    }
}
