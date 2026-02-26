using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;

namespace Methaq.Application.Auth.Queries.GetPendingAccounts;

public class GetPendingAccountsQueryHandler : IRequestHandler<GetPendingAccountsQuery, ErrorOr<List<PendingAccountResponse>>>
{
    private readonly IUserRepository _userRepository;

    public GetPendingAccountsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<PendingAccountResponse>>> Handle(GetPendingAccountsQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetPendingAccountsAsync();

        var response = users.Select(u => new PendingAccountResponse(
            UserId: u.User.Id,
            FullName: u.User.FullName,
            Email: u.User.Email!,
            PhoneNumber: u.User.PhoneNumber!,
            DateOfBirth: u.User.DateOfBirth,
            Role: u.Role
        )).ToList();

        return response;
    }
}