using ErrorOr;
using MediatR;
using Methaq.Application.Common.Emails;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers.enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.Auth.Commands.ApproveAccount;

public class ApproveAccountCommandHandler : IRequestHandler<ApproveAccountCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveAccountCommandHandler(IUserRepository userRepository, IEmailService emailService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(ApproveAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            return ApproveAccountErrors.UserNotFound;

        if (!user.EmailConfirmed)
            return ApproveAccountErrors.EmailNotConfirmed;

        if (user.AccountStatus == AccountStatus.Approved)
            return ApproveAccountErrors.AlreadyApproved;

        user.AccountStatus = AccountStatus.Approved;
        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _emailService.SendEmailAsync(
            user.Email!,
            EmailTemplates.AccountApproved(),
            EmailTemplates.AccountApproved(user.FullName)
            );
        return Result.Success;

    }
}