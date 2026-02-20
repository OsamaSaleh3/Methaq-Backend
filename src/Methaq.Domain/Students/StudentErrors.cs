using ErrorOr;
using System;

namespace Methaq.Domain.Students;

public static class StudentErrors
{
    public static readonly Error UserIdRequired = Error.Validation(
        code: "Student.UserId",
        description: "User ID cannot be empty.");

    public static readonly Error NotAssignedToSection = Error.Conflict(
        code: "Student.NotAssigned",
        description: "Student is not assigned to any section.");

    public static readonly Error TaskNotFound = Error.NotFound(
        code: "Student.TaskNotFound",
        description: "Task not found.");

    public static readonly Error ParentNameRequired = Error.Validation(
        code: "Student.ParentName",
        description: "Parent name is required.");

    public static readonly Error InvalidSectionId = Error.Validation(
        code: "Student.SectionId",
        description: "Section ID is invalid.");

    public static readonly Error TaskCannotBeNull = Error.Validation(
        code: "Student.TaskNull",
        description: "Task cannot be null");
}
