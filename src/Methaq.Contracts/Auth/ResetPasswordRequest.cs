using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Contracts.Auth
{
    public record ResetPasswordRequest(
        string Email,
        string Otp,
        string NewPassword);
}
