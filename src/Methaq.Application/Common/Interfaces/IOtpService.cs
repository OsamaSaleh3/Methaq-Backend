using Methaq.Domain.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Common.Interfaces
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync(ApplicationUser user);
        Task<bool> VerifyAndConfirmEmailAsync(ApplicationUser user, string otp);
    }
}
