using ErrorOr;
using System;

namespace Methaq.Domain.Sections;

public static class SectionErrors
{
    public static readonly Error NameRequired = Error.Validation(
        code: "Section.Name",
        description: "Section name cannot be empty.");

    public static readonly Error SupervisorIdRequired = Error.Validation(
        code: "Section.SupervisorId",
        description: "Supervisor ID is required.");

    public static readonly Error StudentNull = Error.Validation(
        code: "Section.StudentNull",
        description: "Student cannot be null.");

    public static readonly Error StudentExists = Error.Conflict(
        code: "Section.StudentExists",
        description: "Student already in this section.");

    public static readonly Error StudentNotFound = Error.NotFound(
        code: "Section.StudentNotFound",
        description: "Student not found in this section.");

    public static readonly Error SupervisorEmpty = Error.Validation(
        code: "Section.SupervisorEmpty",
        description: "Supervisor cannot be empty.");

    public static readonly Error SameSupervisor = Error.Validation(
        code: "Section.SameSupervisor",
        description: "New supervisor must be different from current.");
}
