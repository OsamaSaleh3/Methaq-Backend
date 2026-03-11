namespace Methaq.Contracts.Students;

public record UpdateGuardianInfoRequest(
    string? GuardianName,
    string? GuardianPhone,
    string? GuardianEmail);