using ErrorOr;
using MediatR;

namespace Methaq.Application.Lectures.Commands.CreateLecture;

public record CreateLectureCommand(
    Guid SectionId,
    DateTime Date,
    TimeOnly StartTime,
    TimeOnly EndTime
) : IRequest<ErrorOr<Guid>>;