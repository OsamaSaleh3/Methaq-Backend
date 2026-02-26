using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Contracts.Auth
{
    public record ConfirmEmailRequest(
    string UserId,
    string Otp
);
}
