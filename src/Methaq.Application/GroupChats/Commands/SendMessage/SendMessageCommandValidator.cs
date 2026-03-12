using FluentValidation;

namespace Methaq.Application.GroupChats.Commands.SendMessage;

public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    private static readonly string[] AllowedExtensions =
        { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".mp4" };

    private const long MaxFileSize = 10 * 1024 * 1024;

    public SendMessageCommandValidator()
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Content) || x.AttachmentStream is not null)
            .WithMessage("Message must have content or attachment.");

        RuleFor(x => x.GroupChatId)
            .NotEmpty().WithMessage("Group chat ID is required.");

        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("Sender ID is required.");
    
        When(x => x.AttachmentStream is not null, () =>
        {
            RuleFor(x => x.AttachmentStream!.Length)
                .LessThanOrEqualTo(MaxFileSize)
                .WithMessage("File size cannot exceed 10MB.");

            RuleFor(x => x.AttachmentFileName)
                .NotEmpty()
                .WithMessage("File name is required when attachment is provided.")
                .Must(fileName => AllowedExtensions.Contains(
                    Path.GetExtension(fileName!).ToLower()))
                .WithMessage("File type not allowed.");
        });
    }
}