using ErrorOr;
using MediatR;
using Methaq.Application.Auth.Commands.Login;
using Methaq.Application.Auth.Commands.Login.Responses;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Interfaces;
using Methaq.Domain.ApplicationUsers.enums;
using Methaq.Domain.RefreshTokens;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IJwtTokenService jwtTokenService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user=await _userRepository.GetByEmailAsync(request.Email);
        if(user is null)
            return LoginErrors.InvalidCredentials;

        var isPasswordValid = await _userRepository.CheckPasswordAsync(user, request.Password);
        if(!isPasswordValid)
            return LoginErrors.InvalidCredentials;

        if (!user.EmailConfirmed)
            return LoginErrors.EmailNotConfirmed;

        if(user.AccountStatus==AccountStatus.Pending)
            return LoginErrors.AccountPending;

        if (user.AccountStatus == AccountStatus.Rejected)
            return LoginErrors.AccountRejected;

        var roles = await _userRepository.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? string.Empty;

        var accessToken = _jwtTokenService.GenerateAccessToken(user, role);
        var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();

        var refreshToken = RefreshToken.Create(
            user.Id,
            refreshTokenValue,
            _jwtTokenService.RefreshTokenExpiryDays);

        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResponse(
            UserId: user.Id,
            FullName: user.FullName,
            Email: user.Email!,
            Role: role,
            AccessToken: accessToken,
            RefreshToken: refreshTokenValue);


    }
}