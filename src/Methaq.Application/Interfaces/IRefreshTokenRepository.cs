using Methaq.Domain.RefreshTokens;

namespace Methaq.Application.Common.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetActiveTokenAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    Task RevokeAllUserTokensAsync(string userId);
}