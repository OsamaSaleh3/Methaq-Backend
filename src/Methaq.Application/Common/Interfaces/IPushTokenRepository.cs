
using Methaq.Domain.PushTokens;

namespace Methaq.Application.Common.Interfaces;

public interface IPushTokenRepository
{
    Task<PushToken?> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task AddAsync(PushToken pushToken, CancellationToken cancellationToken);
    Task<List<string>> GetTokensByUserIdsAsync(List<string> userIds, CancellationToken cancellationToken);
}
