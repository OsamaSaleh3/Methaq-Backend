using ErrorOr;
using MediatR;
using Methaq.Application.Common.Emails;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Interfaces;
using Methaq.Domain.ApplicationUsers.enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.Auth.Commands.RejectAccount;

public class RejectAccountCommandHandler : IRequestHandler<RejectAccountCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public RejectAccountCommandHandler(IUserRepository userRepository, IEmailService emailService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(RejectAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            return RejectAccountErrors.UserNotFound;

        if (user.AccountStatus == AccountStatus.Rejected)
            return RejectAccountErrors.AlreadyRejected;

        if (user.AccountStatus == AccountStatus.Approved)
            return RejectAccountErrors.CannotRejectApproved;

        user.AccountStatus = AccountStatus.Rejected;
        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _emailService.SendEmailAsync(
            user.Email!,
            EmailTemplates.AccountRejected(),
            EmailTemplates.AccountRejected(user.FullName, request.Reason)
            );

        return Result.Success;
    }
}