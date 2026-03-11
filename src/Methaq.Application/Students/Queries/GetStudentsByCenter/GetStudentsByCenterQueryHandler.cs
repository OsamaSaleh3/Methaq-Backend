using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.UseCases.Students.Queries.GetStudentsByCenter;

public class GetStudentsByCenterQueryHandler : IRequestHandler<GetStudentsByCenterQuery, ErrorOr<List<StudentResponse>>>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentsByCenterQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ErrorOr<List<StudentResponse>>> Handle(GetStudentsByCenterQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetByCenterAsync(request.CenterId, cancellationToken);
    return students.Select(student => new StudentResponse(
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
    student.SectionId)).ToList();
    }
}