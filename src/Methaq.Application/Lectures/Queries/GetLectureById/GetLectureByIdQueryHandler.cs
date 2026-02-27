using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Lectures.Queries.GetLectureById;

public class GetLectureByIdQueryHandler : IRequestHandler<GetLectureByIdQuery, ErrorOr<LectureDetailsResponse>>
{
    private readonly ILectureRepository _lectureRepository;

    public GetLectureByIdQueryHandler(ILectureRepository lectureRepository)
    {
        _lectureRepository = lectureRepository;
    }

    public async Task<ErrorOr<LectureDetailsResponse>> Handle(GetLectureByIdQuery query, CancellationToken cancellationToken)
    {
        var lecture = await _lectureRepository.GetByIdWithDetailsAsync(query.LectureId);
        if (lecture is null)
            return Error.NotFound("Lecture.NotFound", "Lecture not found.");

        return new LectureDetailsResponse(
            Id: lecture.Id,
            SectionId: lecture.SectionId,
            SectionName: lecture.Section.Name,
            Date: lecture.Date,
            StartTime: lecture.StartTime.ToString(),
            EndTime: lecture.EndTime.ToString(),
            Status: lecture.Status.ToString(),
            Notes: lecture.Notes,
            AttendanceCount: lecture.AttendanceRecords.Count,
            TasksCount: lecture.SectionTasks.Count);
    }
}