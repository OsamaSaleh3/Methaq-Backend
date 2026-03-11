using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Contracts.Users
{
    public record ChangePasswordRequest(
        string CurrentPassword,
        string NewPassword);
}
