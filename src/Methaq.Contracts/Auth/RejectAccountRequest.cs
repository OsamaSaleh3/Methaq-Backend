using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Contracts.Auth
{
    public record RejectAccountRequest(
    string UserId,
    string? Reason
);
}
