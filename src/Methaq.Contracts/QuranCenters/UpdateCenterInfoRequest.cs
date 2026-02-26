namespace Methaq.Contracts.QuranCenters;

public record UpdateCenterInfoRequest(
    string? Name,
    string? Description,
    string? Location,
    string? PhoneNumber
);
