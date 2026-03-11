using ErrorOr;
using MediatR;

namespace Methaq.Application.UseCases.Students.Commands.RemoveStudentFromCenter;

public record RemoveStudentFromCenterCommand(Guid StudentId) : IRequest<ErrorOr<Success>>;