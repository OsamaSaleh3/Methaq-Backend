using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.PushTokens;

namespace Methaq.Application.Devices.Commands.RegisterPushToken;

public class RegisterPushTokenCommandHandler : IRequestHandler<RegisterPushTokenCommand, ErrorOr<Success>>
{
    private readonly IPushTokenRepository _pushTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPushTokenCommandHandler(
        IPushTokenRepository pushTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _pushTokenRepository = pushTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(RegisterPushTokenCommand request, CancellationToken cancellationToken)
    {
        var existing = await _pushTokenRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        if (existing is null)
        {
            var pushToken = PushToken.Create(request.UserId, request.Token, request.Platform);
            await _pushTokenRepository.AddAsync(pushToken, cancellationToken);
        }
        else
        {
            existing.UpdateToken(request.Token);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}