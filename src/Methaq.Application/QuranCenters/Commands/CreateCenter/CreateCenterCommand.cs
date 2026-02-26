using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Commands.CreateCenter;

public record CreateCenterCommand(
    string Name,
    string Description,
    string Location,
    string? PhoneNumber,
    Guid ManagerId
    ) : IRequest<ErrorOr<Guid>>;
