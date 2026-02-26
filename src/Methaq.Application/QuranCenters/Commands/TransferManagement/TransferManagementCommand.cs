using ErrorOr;
using MediatR;

namespace Methaq.Application.QuranCenters.Commands.TransferManagement;

public record TransferManagementCommand(
    Guid CenterId,
    Guid NewManagerId
) : IRequest<ErrorOr<Success>>;