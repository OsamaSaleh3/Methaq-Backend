namespace Methaq.Contracts.GroupChats;

public record SendMessageRequest(
    string Content,
    string? AttachmentUrl
);