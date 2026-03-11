using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Students;

namespace Methaq.Application.UseCases.Students.Queries.GetStudentById;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, ErrorOr<StudentResponse>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ErrorOr<StudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdWithUserAsync(request.StudentId, cancellationToken);
        if (student is null)
            return StudentErrors.NotFound;

        return new StudentResponse(
            student.Id,
            student.UserId,
            student.User.FullName,
            student.User.Email!,
            student.User.PhoneNumber,
            student.GuardianName,
            student.GuardianPhone,
            student.GuardianEmail,
            student.AcademicLevel,
            student.CenterId,
            student.SectionId);
    }
}