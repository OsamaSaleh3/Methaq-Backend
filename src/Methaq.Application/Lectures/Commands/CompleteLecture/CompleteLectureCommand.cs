using ErrorOr;
using MediatR;

namespace Methaq.Application.Lectures.Commands.CompleteLecture;

public record CompleteLectureCommand(
    Guid LectureId,
    string? Notes
) : IRequest<ErrorOr<Success>>;