using ErrorOr;
using MediatR;
using Methaq.Application.Common.Emails;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.ApplicationUsers.enums;
using Methaq.Domain.Students;
using Microsoft.AspNetCore.Identity;

namespace Methaq.Application.Auth.Commands.RegisterStudent;

public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, ErrorOr<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IOtpService _otpService;
    private readonly IUserRepository _userRepository;

    public RegisterStudentCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService, IOtpService otpService, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _otpService = otpService;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<string>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.IsEmailExistsAsync(request.Email);
        if(!existingUser)
            return RegisterStudentErrors.EmailAlreadyExists;

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            SecondName = request.SecondName,
            ThirdName = request.ThirdName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
            NationalId = request.NationalId,
            Address = request.Address,
            AccountStatus = AccountStatus.Pending
        };

        var studentResult = Student.Create(
            user.Id,
            request.GuardianName,
            request.GuardianPhone,
            request.GuardianEmail,
            request.AcademicLevel
            );

        if(studentResult.IsError)
            return studentResult.Errors;

        var student=studentResult.Value;

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var createUserResult = await _userRepository.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
                return createUserResult.Errors.Select(e => Error.Validation(e.Code, e.Description))
                    .ToList();

            await _userRepository.AddToRoleAsync(user, "Student");
            await _userRepository.AddStudentAsync(student, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            return RegisterStudentErrors.RegisterFailed;
        }

        var otp = await _otpService.GenerateOtpAsync(user);
        await _emailService.SendEmailAsync(
            user.Email,
           EmailTemplates.OtpConfirmation(),
            EmailTemplates.OtpConfirmation(user.FullName, otp)
            );

        return user.Id;
    }
}