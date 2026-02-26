using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Commands.RemoveSupervisor;

public record RemoveSupervisorCommand(
    Guid CenterId,
    Guid SupervisorId
) : IRequest<ErrorOr<Success>>;
