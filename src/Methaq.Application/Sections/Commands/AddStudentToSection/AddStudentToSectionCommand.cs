using ErrorOr;
using MediatR;

namespace Methaq.Application.Sections.Commands.AddStudentToSection;

public record AddStudentToSectionCommand(
    Guid SectionId,
    Guid StudentId
) : IRequest<ErrorOr<Success>>;