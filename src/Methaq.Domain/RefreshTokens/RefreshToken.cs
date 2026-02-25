using Methaq.Domain.ApplicationUsers;
using Methaq.Domain.Common;

namespace Methaq.Domain.RefreshTokens;

public class RefreshToken : BaseEntity
{
    public string UserId { get; private set; } = null!;
    public ApplicationUser User { get; private set; } = null!;
    public string Token { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }

    protected RefreshToken() { }

    private RefreshToken(string userId, string token, DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        IsRevoked = false;
    }

    public static RefreshToken Create(string userId, string token, int expiryDays)
    {
        return new RefreshToken(userId, token, DateTime.UtcNow.AddDays(expiryDays));
    }

    public void Revoke()
    {
        IsRevoked = true;
        MarkAsUpdated();
    }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsActive => !IsRevoked && !IsExpired;
}