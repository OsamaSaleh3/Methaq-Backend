using Microsoft.AspNetCore.Http;

namespace Methaq.Contracts.GroupChats;

public record SendMessageRequest(
    string Content,
    IFormFile? Attachment);