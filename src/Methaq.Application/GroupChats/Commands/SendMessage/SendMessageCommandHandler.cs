using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.GroupChats;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.GroupChats.Commands.SendMessage;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ErrorOr<MessageDto>>
{
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IChatSender _chatSender;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageCommandHandler(IGroupChatRepository groupChatRepository, IUserRepository userRepository, IChatSender chatSender, IUnitOfWork unitOfWork)
    {
        _groupChatRepository = groupChatRepository;
        _userRepository = userRepository;
        _chatSender = chatSender;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<MessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await _groupChatRepository.GetByIdWithMembersAsync(request.GroupChatId);
        if (chat is null)
            return SendMessageErrors.ChatNotFound;

        var sender = await _userRepository.GetByIdAsync(request.SenderId);
        if (sender is null)
            return SendMessageErrors.SenderNotFound;

        if (!chat.Members.Any(m => m.Id == request.SenderId))
            return SendMessageErrors.SenderNotMember;

        var messageResult = GroupMessage.Create(
            request.GroupChatId,
            request.SenderId,
            request.Content,
            request.AttachmentUrl
            );
        if (messageResult.IsError)
            return messageResult.Errors;

        var message = messageResult.Value;

        var sendResult = chat.SendMessage(message);
        if (sendResult.IsError)
            return sendResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var messageDto = new MessageDto(
           message.Id,
           message.GroupChatId,
           message.SenderId,
           sender.FullName,
           message.Content,
           message.AttachmentUrl,
           message.CreatedAt);

        await _chatSender.SendMessageAsync(request.GroupChatId, messageDto);

        return messageDto;

    }
}
