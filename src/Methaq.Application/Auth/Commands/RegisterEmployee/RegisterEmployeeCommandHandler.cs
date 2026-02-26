using ErrorOr;
using MediatR;
using Methaq.Application.Common.Emails;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.ApplicationUsers.enums;
using Methaq.Domain.Employees;

namespace Methaq.Application.Auth.Commands.RegisterEmployee;

public class RegisterEmployeeCommandHandler : IRequestHandler<RegisterEmployeeCommand, ErrorOr<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IOtpService _otpService;

    public RegisterEmployeeCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEmailService emailService, IOtpService otpService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _otpService = otpService;
    }

    public async Task<ErrorOr<string>> Handle(RegisterEmployeeCommand request, CancellationToken cancellationToken)
    {
        var existingEmail=await _userRepository.IsEmailExistsAsync(request.Email);
        if(!existingEmail)
        {
            return RegisterEmployeeErrors.EmailAlreadyExists;
        }

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

        var employeeResult = Employee.Create(
                user.Id,
                request.Degree,
                request.Specialization,
                request.IslamicQualifications,
                request.CurrentJob,
                request.Role
            );

        if (employeeResult.IsError)
            return employeeResult.Errors;

        var employee = employeeResult.Value;

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var createUserResult=await _userRepository.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
                return createUserResult.Errors.Select(e => Error.Validation(e.Code, e.Description))
                    .ToList();

            await _userRepository.AddToRoleAsync(user, request.Role.ToString());
            await _userRepository.AddEmployeeAsync(employee,cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            return RegisterEmployeeErrors.RegisterFailed;
        }

        var otp =await _otpService.GenerateOtpAsync(user);
        await _emailService.SendEmailAsync(
                request.Email,
                EmailTemplates.OtpConfirmation(),
                EmailTemplates.OtpConfirmation(user.FullName, otp)
            );

        return user.Id;
    }
}