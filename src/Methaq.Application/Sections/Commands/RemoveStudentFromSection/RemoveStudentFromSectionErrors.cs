using ErrorOr;

namespace Methaq.Application.Sections.Commands.RemoveStudentFromSection;

public static class RemoveStudentFromSectionErrors
{
    public static readonly Error SectionNotFound = Error.NotFound(
        code: "Section.NotFound",
        description: "Section not found.");

    public static readonly Error StudentNotFound = Error.NotFound(
        code: "Section.StudentNotFound",
        description: "Student not found in this section.");
}