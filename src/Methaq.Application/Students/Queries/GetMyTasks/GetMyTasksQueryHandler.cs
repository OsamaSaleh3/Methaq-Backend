using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Students;

namespace Methaq.Application.UseCases.Students.Queries.GetMyTasks;

public class GetMyTasksQueryHandler : IRequestHandler<GetMyTasksQuery, ErrorOr<List<StudentTaskResponse>>>
{
    private readonly IStudentRepository _studentRepository;

    public GetMyTasksQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ErrorOr<List<StudentTaskResponse>>> Handle(GetMyTasksQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        if (student is null)
            return StudentErrors.NotFound;

        if (student.SectionId is null)
            return StudentErrors.NotAssignedToSection;

        var tasks = await _studentRepository.GetMyTasksAsync(student.Id, cancellationToken);

        return tasks.Select(t => new StudentTaskResponse(
            t.Id,
            t.Title,
            t.Description,
            t.Types,
            t.Status,
            t.FullMark,
            t.Evaluations.FirstOrDefault(e => e.StudentId == student.Id)?.AchievedMark)).ToList();
    }
}