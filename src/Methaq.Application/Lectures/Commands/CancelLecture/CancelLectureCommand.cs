using ErrorOr;
using MediatR;

namespace Methaq.Application.Lectures.Commands.CancelLecture;

public record CancelLectureCommand(
    Guid LectureId
) : IRequest<ErrorOr<Success>>;