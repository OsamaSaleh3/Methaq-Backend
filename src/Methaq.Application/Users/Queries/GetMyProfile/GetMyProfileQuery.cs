using ErrorOr;
using MediatR;
using Methaq.Domain.Employees.enums;

namespace Methaq.Application.Users.Queries.GetMyProfile;

public record GetMyProfileQuery(string UserId) : IRequest<ErrorOr<ProfileResponse>>;

 public record ProfileResponse(
     string Id,
     string FirstName,
     string SecondName,
     string ThirdName,
     string LastName,
     string FullName,
     string Email,
     string? PhoneNumber,
     string? NationalId,
     DateOnly DateOfBirth,
     string? Address,
     StudentProfileInfo? StudentInfo,
     EmployeeProfileInfo? EmployeeInfo
     );

public record StudentProfileInfo(
    Guid StudentId,
    string GuardianName,
    string GuardianPhone,
    string? GuardianEmail,
    string AcademicLevel,
    Guid? CenterId,
    Guid? SectionId);

public record EmployeeProfileInfo(
    Guid EmployeeId,
    string Specialization,
    string? IslamicQualifications,
    string? CurrentJob,
    AcademicDegree Degree,
    EmployeeRole Role,
    EmploymentStatus EmploymentStatus,
    Guid? CenterId);