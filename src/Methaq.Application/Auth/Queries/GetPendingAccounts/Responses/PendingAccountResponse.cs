using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Auth.Queries.GetPendingAccounts.Responses
{
    public record PendingAccountResponse(
    string UserId,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    string Role
);
}
