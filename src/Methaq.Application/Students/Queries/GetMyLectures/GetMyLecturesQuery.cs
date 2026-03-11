using ErrorOr;
using MediatR;
using Methaq.Domain.Lectures.enums;

namespace Methaq.Application.UseCases.Students.Queries.GetMyLectures;

public record GetMyLecturesQuery(string UserId) : IRequest<ErrorOr<List<StudentLectureResponse>>>;

public record StudentLectureResponse(
    Guid Id,
    DateTime Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    LectureStatus Status,
    string? Notes);