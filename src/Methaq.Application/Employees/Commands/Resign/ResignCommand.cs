using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Resign
{
    public record ResignCommand(Guid EmployeeId) : IRequest<ErrorOr<Success>>;
}
