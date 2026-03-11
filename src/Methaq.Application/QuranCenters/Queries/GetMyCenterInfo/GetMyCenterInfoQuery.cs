using ErrorOr;
using MediatR;
using Methaq.Domain.QuranCenters.enums;

namespace Methaq.Application.UseCases.QuranCenters.Queries.GetMyCenterInfo;

public record GetMyCenterInfoQuery(string UserId) : IRequest<ErrorOr<CenterInfoResponse>>;


public record CenterInfoResponse(
    Guid Id,
    string Name,
    string Description,
    string Location,
    string? PhoneNumber,
    CenterStatus Status,
    Guid ManagerId,
    int SectionsCount,
    int SupervisorsCount);