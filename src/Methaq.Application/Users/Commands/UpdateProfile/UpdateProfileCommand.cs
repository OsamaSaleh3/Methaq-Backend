using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Users.Commands.UpdateProfile
{
    public record UpdateProfileCommand(
     string UserId,
     string? FirstName,
     string? SecondName,
     string? ThirdName,
     string? LastName,
     string? PhoneNumber,
     string? Address) : IRequest<ErrorOr<Success>>;
}
