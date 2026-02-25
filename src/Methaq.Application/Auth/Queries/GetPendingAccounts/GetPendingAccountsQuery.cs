using ErrorOr;
using MediatR;
using Methaq.Application.Auth.Queries.GetPendingAccounts.Responses;

public record GetPendingAccountsQuery() : IRequest<ErrorOr<List<PendingAccountResponse>>>;
