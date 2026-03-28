using ErrorOr;
using MediatR;

namespace Methaq.Application.Lectures.Commands.CreateLecture;

public record CreateLectureCommand(
    Guid SectionId,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime
) : IRequest<ErrorOr<Guid>>;