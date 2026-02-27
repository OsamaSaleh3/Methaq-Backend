using ErrorOr;
using MediatR;

namespace Methaq.Application.Sections.Commands.RemoveStudentFromSection;

public record RemoveStudentFromSectionCommand(
    Guid SectionId,
    Guid StudentId
) : IRequest<ErrorOr<Success>>;