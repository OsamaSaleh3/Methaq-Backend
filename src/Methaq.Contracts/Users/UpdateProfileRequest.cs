using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Contracts.Users
{
    public record UpdateProfileRequest(
    string? FirstName,
    string? SecondName,
    string? ThirdName,
    string? LastName,
    string? PhoneNumber,
    string? Address);
}
