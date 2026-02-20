using ErrorOr;
using System;

namespace Methaq.Domain.Employees;

public static class EmployeeErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        code: "Employee.UserId",
        description: "User ID cannot be empty.");

    public static readonly Error AlreadyResigned = Error.Conflict(
        code: "Employee.AlreadyResigned",
        description: "Employee is already resigned.");

    public static readonly Error CannotUpdateResigned = Error.Conflict(
        code: "Employee.CannotUpdateResigned",
        description: "Cannot update resigned employee.");

    public static readonly Error NotResigned = Error.Validation(
        code: "Employee.NotResigned",
        description: "Only resigned employees can be reactivated.");
}
