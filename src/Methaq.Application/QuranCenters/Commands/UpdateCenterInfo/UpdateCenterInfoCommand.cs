using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Commands.UpdateCenterInfo;

public record UpdateCenterInfoCommand(
    Guid CenterId,
    string? Name,
    string? Description,
    string? Location,
    string? PhoneNumber
) : IRequest<ErrorOr<Success>>;
