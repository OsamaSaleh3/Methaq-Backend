using Methaq.Domain.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Interfaces
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync(ApplicationUser user);
        Task<bool> VerifyOtpAsync(ApplicationUser user, string otp);
    }
}
