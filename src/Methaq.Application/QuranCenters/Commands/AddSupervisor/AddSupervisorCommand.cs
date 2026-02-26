using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Commands.AddSupervisor;

public record AddSupervisorCommand(
    Guid CenterId,
    Guid SupervisorId
    ) : IRequest<ErrorOr<Success>>;
