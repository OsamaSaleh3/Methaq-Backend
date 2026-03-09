using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Reactivate
{
    public record ReactivateCommand(Guid EmployeeId) : IRequest<ErrorOr<Success>>;
}
