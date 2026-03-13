using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;

namespace Methaq.Application.Auth.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpService _otpService;

    public ResetPasswordCommandHandler(IUserRepository userRepository, IOtpService otpService)
    {
        _userRepository = userRepository;
        _otpService = otpService;
    }

    public async Task<ErrorOr<Success>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            return ResetPasswordErrors.InvalidOtp;

        var isValid = await _otpService.VerifyOtpAsync(user, request.Otp);
        if (!isValid)
            return ResetPasswordErrors.InvalidOtp;

        var token = await _userRepository.GeneratePasswordResetTokenAsync(user);
        var result = await _userRepository.ResetPasswordAsync(user, token, request.NewPassword);
       
        if (!result.Succeeded)
            return ResetPasswordErrors.ResetFailed;

        return Result.Success;
    }
}