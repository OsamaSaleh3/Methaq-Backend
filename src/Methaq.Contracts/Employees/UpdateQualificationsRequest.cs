using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Contracts.Employees
{
    public record UpdateQualificationsRequest(
        int? AcademicDegree,
        string? Spesialization,
        string? IslamicAualification
        );
}
