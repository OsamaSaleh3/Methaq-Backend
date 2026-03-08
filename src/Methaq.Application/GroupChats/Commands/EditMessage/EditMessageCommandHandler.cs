using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.GroupChats.Commands.EditMessage;

public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand, ErrorOr<Success>>
{
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly IChatSender _chatSender;
    private readonly IUnitOfWork _unitOfWork;

    public EditMessageCommandHandler(
        IGroupChatRepository groupChatRepository,
        IChatSender chatSender,
        IUnitOfWork unitOfWork)
    {
        _groupChatRepository = groupChatRepository;
        _chatSender = chatSender;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(EditMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _groupChatRepository.GetMessageByIdAsync(request.MessageId);
        if (message is null)
            return EditMessageErrors.MessageNotFound;

        if (message.SenderId != request.UserId)
            return EditMessageErrors.Unauthorized;

        var editResult = message.Edit(request.NewContent,request.UserId);
        if (editResult.IsError)
            return editResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _chatSender.EditMessageAsync(message.GroupChatId, message.Id, request.NewContent);

        return Result.Success;
    }
}