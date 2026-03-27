using ErrorOr;
using MediatR;

public record GetPendingAccountsQuery() : IRequest<ErrorOr<List<PendingAccountResponse>>>;

public record PendingAccountResponse(
   string UserId,
   string FullName,
   string Email,
   string PhoneNumber,
   DateOnly DateOfBirth,
   string Role
);