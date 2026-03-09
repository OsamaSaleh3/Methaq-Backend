using ErrorOr;
using MediatR;
using Methaq.Domain.Employees.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.UpdateQualifications
{
    public record UpdateQualificationsCommand(
        Guid EmployeeId,
        AcademicDegree? Degree,
        string? Specialization,
        string? IslamicQualifications
        ) : IRequest<ErrorOr<Success>>;
}
