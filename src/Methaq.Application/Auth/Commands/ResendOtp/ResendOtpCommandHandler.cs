using ErrorOr;
using MediatR;
using Methaq.Application.Common.Emails;
using Methaq.Application.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.Auth.Commands.ResendOtp;

public class ResendOtpCommandHandler : IRequestHandler<ResendOtpCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpService _otpService;
    private readonly IEmailService _emailService;

    public ResendOtpCommandHandler(IUserRepository userRepository, IOtpService otpService, IEmailService emailService)
    {
        _userRepository = userRepository;
        _otpService = otpService;
        _emailService = emailService;
    }

    public async Task<ErrorOr<Success>> Handle(ResendOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            return ResendOtpErrors.UserNotFound;

        if (user.EmailConfirmed)
            return ResendOtpErrors.EmailAlreadyConfirmed;

        var otp = await _otpService.GenerateOtpAsync(user);
        await _emailService.SendEmailAsync(
            EmailTemplates.OtpConfirmation(),
            EmailTemplates.OtpConfirmation(user.FullName,otp),
            otp);

        return Result.Success;
    }
}