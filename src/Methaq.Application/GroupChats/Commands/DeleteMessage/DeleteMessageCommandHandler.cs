using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.GroupChats.Commands.DeleteMessage;

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, ErrorOr<Success>>
{
    private readonly IGroupChatRepository _groupChatRepository;
    private readonly IChatSender _chatSender;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public DeleteMessageCommandHandler(IGroupChatRepository groupChatRepository, IChatSender chatSender, IUnitOfWork unitOfWork, IFileService fileService)
    {
        _groupChatRepository = groupChatRepository;
        _chatSender = chatSender;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<ErrorOr<Success>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _groupChatRepository.GetMessageByIdAsync(request.MessageId);
        if (message is null)
            return DeleteMessageErrors.MessageNotFound;

        if (message.SenderId != request.UserId)
            return DeleteMessageErrors.Unauthorized;

        var attachmentUrl = message.AttachmentUrl;


        var deleteResult = message.Delete(request.UserId);
        if (deleteResult.IsError)
            return deleteResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrEmpty(attachmentUrl))
            await _fileService.DeleteAsync(attachmentUrl);

        await _chatSender.DeleteMessageAsync(message.GroupChatId, message.Id);

        return Result.Success;
    }
}