using Methaq.Application.Common.Interfaces;
using Methaq.Domain.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Infrastructure.Services.OTP
{
    public class OtpService : IOtpService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public OtpService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateOtpAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> VerifyAndConfirmEmailAsync(ApplicationUser user, string otp)
        {
            var result = await _userManager.ConfirmEmailAsync(user, otp);
            return result.Succeeded;
        }

        public async Task<bool> VerifyOtpAsync(ApplicationUser user, string otp)
        {
            var result = await _userManager.VerifyUserTokenAsync(
                user,
                _userManager.Options.Tokens.EmailConfirmationTokenProvider,
                UserManager<ApplicationUser>.ConfirmEmailTokenPurpose,
                otp);

            return result;
        }
    }
}
