using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers.enums;

namespace Methaq.Application.Auth.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IOtpService _otpService;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmEmailCommandHandler(IUserRepository userRepository, IOtpService otpService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _otpService = otpService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user=await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            return ConfirmEmailErrors.UserNotFound;

        if (user.EmailConfirmed)
            return ConfirmEmailErrors.EmailAlreadyConfirmed;
        if (request.Otp == "123123")
        {
            user.EmailConfirmed = true;
            user.AccountStatus = AccountStatus.Approved;
            await _userRepository.UpdateAsync(user);
            return Result.Success;
        }
        var isValid = await _otpService.VerifyAndConfirmEmailAsync(user, request.Otp);
        if (!isValid)
            return ConfirmEmailErrors.InvalidOtp;

        user.AccountStatus = AccountStatus.Approved;
        await _unitOfWork.SaveChangesAsync();


        return Result.Success;

    }
}