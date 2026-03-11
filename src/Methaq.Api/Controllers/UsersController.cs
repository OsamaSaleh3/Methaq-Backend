using MediatR;
using Methaq.Application.Users.Queries.GetMyProfile;
using Methaq.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var query = new GetMyProfileQuery(UserId);
            var result = await _sender.Send(query);
            return HandleResult(result);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
        {
            var command = new UpdateProfileCommand(
                UserId,
                request.FirstName,
                request.SecondName,
                request.ThirdName,
                request.LastName,
                request.PhoneNumber,
                request.Address);
            var result = await _sender.Send(command);
            return HandleResult(result);
        }

        [HttpPut("me/change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var command = new ChangePasswordCommand(UserId, request.CurrentPassword, request.NewPassword);
            var result = await _sender.Send(command);
            return HandleResult(result);
        }
    }
}
