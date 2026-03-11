using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Users.Commands.UpdateProfile
{
    internal class UpdateProfileErrors
    {
        public static readonly Error NotFound = Error.NotFound(
        code: "User.NotFound",
        description: "User not found.");

        public static readonly Error UpdateFailed = Error.Failure(
            code: "User.UpdateFailed",
            description: "Failed to update user profile.");
    }
}
