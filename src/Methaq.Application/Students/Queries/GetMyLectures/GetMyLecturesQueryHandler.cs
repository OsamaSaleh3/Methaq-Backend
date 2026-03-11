using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Students;

namespace Methaq.Application.UseCases.Students.Queries.GetMyLectures;

public class GetMyLecturesQueryHandler : IRequestHandler<GetMyLecturesQuery, ErrorOr<List<StudentLectureResponse>>>
{
    private readonly IStudentRepository _studentRepository;

    public GetMyLecturesQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ErrorOr<List<StudentLectureResponse>>> Handle(GetMyLecturesQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (student is null)
            return StudentErrors.NotFound;

        if (student.SectionId is null)
            return StudentErrors.NotAssignedToSection;

        var lectures = await _studentRepository.GetMyLecturesAsync(student.Id, cancellationToken);

        return lectures.Select(l => new StudentLectureResponse(
            l.Id,
            l.Date,
            l.StartTime,
            l.EndTime,
            l.Status,
            l.Notes)).ToList();
    }
}