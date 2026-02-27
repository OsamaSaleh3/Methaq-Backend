using ErrorOr;
using MediatR;

namespace Methaq.Application.Sections.Commands.ChangeSupervisor;

public record ChangeSupervisorCommand(
    Guid SectionId,
    Guid NewSupervisorId
) : IRequest<ErrorOr<Success>>;