using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Users.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, ErrorOr<ProfileResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetMyProfileQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<ProfileResponse>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserProfileAsync(request.UserId);
                

            if (user is null)
                return Error.NotFound("User.NotFound", "User not found.");

            StudentProfileInfo? studentInfo = user.Student is null ? null : new StudentProfileInfo(
                user.Student.Id,
                user.Student.GuardianName,
                user.Student.GuardianPhone,
                user.Student.GuardianEmail,
                user.Student.AcademicLevel,
                user.Student.CenterId,
                user.Student.SectionId);

            EmployeeProfileInfo? employeeInfo = user.Employee is null ? null : new EmployeeProfileInfo(
                user.Employee.Id,
                user.Employee.Specialization,
                user.Employee.IslamicQualifications,
                user.Employee.CurrentJob,
                user.Employee.Degree,
                user.Employee.Role,
                user.Employee.EmploymentStatus,
                user.Employee.CenterId);

            return new ProfileResponse(
                user.Id,
                user.FirstName,
                user.SecondName,
                user.ThirdName,
                user.LastName,
                user.FullName,
                user.Email!,
                user.PhoneNumber,
                user.NationalId,
                user.DateOfBirth,
                user.Address,
                studentInfo,
                employeeInfo);
        }
    }
}
