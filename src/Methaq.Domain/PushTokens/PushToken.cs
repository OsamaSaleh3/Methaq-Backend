using Methaq.Domain.Common;

namespace Methaq.Domain.PushTokens;

public class PushToken : BaseEntity
{
    public string UserId { get; private set; } = null!;
    public string Token { get; private set; } = null!;
    public string Platform { get; private set; } = null!;
    public DateTime LastUpdatedAt { get; private set; }

    protected PushToken() { }

    private PushToken(string userId, string token, string platform)
    {
        UserId = userId;
        Token = token;
        Platform = platform;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public static PushToken Create(string userId, string token, string platform)
        => new(userId, token, platform);

    public void UpdateToken(string newToken)
    {
        Token = newToken;
        LastUpdatedAt = DateTime.UtcNow;
        MarkAsUpdated();
    }
}