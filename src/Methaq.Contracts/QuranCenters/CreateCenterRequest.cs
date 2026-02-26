namespace Methaq.Contracts.QuranCenters;

public record CreateCenterRequest(
    string Name,
    string Description,
    string Location,
    string? PhoneNumber,
    Guid ManagerId
);
