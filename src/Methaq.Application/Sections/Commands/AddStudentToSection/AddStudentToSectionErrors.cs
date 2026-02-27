using ErrorOr;

namespace Methaq.Application.Sections.Commands.AddStudentToSection;

public static class AddStudentToSectionErrors
{
    public static readonly Error SectionNotFound = Error.NotFound(
        code: "Section.NotFound",
        description: "Section not found.");

    public static readonly Error StudentNotFound = Error.NotFound(
        code: "Section.StudentNotFound",
        description: "Student not found.");

    public static readonly Error StudentNotEnrolledInCenter = Error.Conflict(
        code: "Section.StudentNotEnrolledInCenter",
        description: "Student is not enrolled in this center.");
}