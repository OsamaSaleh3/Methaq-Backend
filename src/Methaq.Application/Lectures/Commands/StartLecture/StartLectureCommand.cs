using ErrorOr;
using MediatR;

namespace Methaq.Application.Lectures.Commands.StartLecture;

public record StartLectureCommand(
    Guid LectureId
) : IRequest<ErrorOr<Success>>;