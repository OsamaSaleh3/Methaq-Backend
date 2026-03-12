using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.GroupChats;

namespace Methaq.Application.GroupChats.Commands.MarkChatAsRead;

public class MarkChatAsReadCommandHandler : IRequestHandler<MarkChatAsReadCommand, ErrorOr<Success>>
{
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkChatAsReadCommandHandler(IGroupChatRepository groupChatRepository, IUnitOfWork unitOfWork)
    {
        _groupChatRepository = groupChatRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(MarkChatAsReadCommand request, CancellationToken cancellationToken)
    {
        var chat = await _groupChatRepository.GetByIdAsync(request.GroupChatId);
        if (chat is null)
            return MarkChatAsReadErrors.ChatNotFound;

        var message = await _groupChatRepository.GetMessageByIdAsync(request.LastMessageId);
        if (message is null)
            return MarkChatAsReadErrors.MessageNotFound;

        if (message.GroupChatId != request.GroupChatId)
            return MarkChatAsReadErrors.MessageNotInChat;

        var lastRead = await _groupChatRepository.GetLastReadAsync(request.UserId, request.GroupChatId, cancellationToken);

        if (lastRead is null)
        {
            var newLastRead = UserChatLastRead.Create(request.UserId, request.GroupChatId, request.LastMessageId);
            await _groupChatRepository.AddLastReadAsync(newLastRead, cancellationToken);
        }
        else
        {
            lastRead.UpdateLastRead(request.LastMessageId);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}