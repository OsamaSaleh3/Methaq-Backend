using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.RefreshTokens;

namespace Methaq.Application.Auth.Commands.RefreshTokens;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ErrorOr<RefreshTokenResponse>>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IJwtTokenService jwtTokenService, IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetActiveTokenAsync(request.RefreshToken);
        if (refreshToken is null || refreshToken.IsExpired)
            return RefreshTokenErrors.InvalidRefreshToken;

        refreshToken.Revoke();

        var roles = await _userRepository.GetRolesAsync(refreshToken.User);
        var role = roles.FirstOrDefault() ?? string.Empty;

        var newAccessToken = _jwtTokenService.GenerateAccessToken(refreshToken.User, role);
        var newRefreshTokenValue = _jwtTokenService.GenerateRefreshToken();

        var newRefreshToken = RefreshToken.Create(
           refreshToken.UserId,
           newRefreshTokenValue,
           _jwtTokenService.RefreshTokenExpiryDays);

        await _refreshTokenRepository.AddAsync(newRefreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RefreshTokenResponse(
            AccessToken: newAccessToken,
            RefreshToken: newRefreshTokenValue);


    }
}