using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Commands.CloseCenter;

public record CloseCenterCommand(
    Guid CenterId
) : IRequest<ErrorOr<Success>>;