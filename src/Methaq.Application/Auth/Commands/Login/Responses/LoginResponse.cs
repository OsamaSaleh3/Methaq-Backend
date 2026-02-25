using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Auth.Commands.Login.Responses
{
    public record LoginResponse(
    string UserId,
    string FullName,
    string Email,
    string Role,
    string AccessToken,
    string RefreshToken
);
}
