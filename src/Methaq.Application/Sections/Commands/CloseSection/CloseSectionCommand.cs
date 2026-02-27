using ErrorOr;
using MediatR;

namespace Methaq.Application.Sections.Commands.CloseSection;

public record CloseSectionCommand(
    Guid SectionId
) : IRequest<ErrorOr<Success>>;