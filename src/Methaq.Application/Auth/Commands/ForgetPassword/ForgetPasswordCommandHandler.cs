using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers;
using Methaq.Application.Common.Emails;

namespace Methaq.Application.Auth.Commands.ForgetPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpService _otpService;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(IUserRepository userRepository, IOtpService otpService, IEmailService emailService)
    {
        _userRepository = userRepository;
        _otpService = otpService;
        _emailService = emailService;
    }

    public async Task<ErrorOr<Success>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return Result.Success;

        var otp = await _otpService.GenerateOtpAsync(user);

        await _emailService.SendEmailAsync(
            request.Email,
            EmailTemplates.ForgotPassword(),
            EmailTemplates.ForgotPassword(user.FullName, otp)
            );

        return Result.Success;
    }
}