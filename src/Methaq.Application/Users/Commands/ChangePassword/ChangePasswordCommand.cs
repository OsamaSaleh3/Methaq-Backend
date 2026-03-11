using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(
     string UserId,
     string CurrentPassword,
     string NewPassword) : IRequest<ErrorOr<Success>>;
}
