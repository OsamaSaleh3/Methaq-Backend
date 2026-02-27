using ErrorOr;

namespace Methaq.Application.Sections.Commands.CloseSection;

public static class CloseSectionErrors
{
    public static readonly Error SectionNotFound = Error.NotFound(
        code: "Section.NotFound",
        description: "Section not found.");
}