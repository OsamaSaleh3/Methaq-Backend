using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Students;

namespace Methaq.Application.UseCases.Students.Queries.GetMySection;

public class GetMySectionQueryHandler : IRequestHandler<GetMySectionQuery, ErrorOr<StudentSectionResponse>>
{
    private readonly IStudentRepository _studentRepository;

    public GetMySectionQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ErrorOr<StudentSectionResponse>> Handle(GetMySectionQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByUserIdWithSectionAsync(request.UserId, cancellationToken);
        if (student is null)
            return StudentErrors.NotFound;

        if (student.SectionId is null || student.Section is null)
            return StudentErrors.NotAssignedToSection;

        return new StudentSectionResponse(
            student.Section.Id,
            student.Section.Name,
            student.Section.AcademicLevel,
            student.Section.Status,
            student.Section.SupervisorId);
    }
}