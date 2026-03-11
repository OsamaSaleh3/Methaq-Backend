using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Students.Commands.UpdateGuardianInfo;

public record UpdateGuardianInfoCommand(
    string UserId,
    string? GuardianName,
    string? GuardianPhone,
    string? GuardianEmail) : IRequest<ErrorOr<Success>>;