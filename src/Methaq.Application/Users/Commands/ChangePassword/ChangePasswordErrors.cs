using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordErrors
    {
        public static readonly Error NotFound = Error.NotFound(
        code: "User.NotFound",
        description: "User not found.");

        public static readonly Error InvalidCurrentPassword = Error.Validation(
            code: "User.InvalidPassword",
            description: "Current password is incorrect.");
    }
}
