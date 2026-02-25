using Methaq.Domain.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(ApplicationUser user, string role);
        string GenerateRefreshToken();
        int RefreshTokenExpiryDays { get; }

    }
}
