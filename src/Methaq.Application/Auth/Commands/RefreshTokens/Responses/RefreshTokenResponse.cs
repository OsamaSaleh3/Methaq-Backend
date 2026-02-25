using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Auth.Commands.RefreshTokens.Responses
{
    public record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken
);
}
