using MediatR;
using Methaq.Application.Auth.Commands.ApproveAccount;
using Methaq.Application.Auth.Commands.ConfirmEmail;
using Methaq.Application.Auth.Commands.ForgetPassword;
using Methaq.Application.Auth.Commands.Login;
using Methaq.Application.Auth.Commands.RefreshTokens;
using Methaq.Application.Auth.Commands.RegisterEmployee;
using Methaq.Application.Auth.Commands.RegisterStudent;
using Methaq.Application.Auth.Commands.RejectAccount;
using Methaq.Application.Auth.Commands.ResendOtp;
using Methaq.Application.Auth.Commands.ResetPassword;
using Methaq.Application.Auth.Queries.GetPendingAccounts;
using Methaq.Contracts.Auth;
using Methaq.Domain.Employees.enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Methaq.Api.Controllers;

public class AuthController : BaseController
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("register/student")]
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentRequest request)
    {
        var command = new RegisterStudentCommand(
            request.FirstName,
            request.SecondName,
            request.ThirdName,
            request.LastName,
            request.Email,
            request.Password,
            request.ConfirmPassword,
            request.PhoneNumber,
            request.DateOfBirth,
            request.NationalId,
            request.Address,
            request.GuardianName,
            request.GuardianPhone,
            request.GuardianEmail,
            request.AcademicLevel);

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpPost("register/employee")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterEmployeeRequest request)
    {
        var command = new RegisterEmployeeCommand(
            request.FirstName,
            request.SecondName,
            request.ThirdName,
            request.LastName,
            request.Email,
            request.Password,
            request.ConfirmPassword,
            request.PhoneNumber,
            request.DateOfBirth,
            request.NationalId,
            request.Address,
             (AcademicDegree)(request.Degree),
            request.Specialization,
            request.IslamicQualifications,
            request.CurrentJob,
             (EmployeeRole)(request.Role));

        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        var command = new ConfirmEmailCommand(request.UserId, request.Otp);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpPost("resend-otp")]
    public async Task<IActionResult> ResendOtp([FromBody] ResendOtpRequest request)
    {
        var command = new ResendOtpCommand(request.UserId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand(request.RefreshToken);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost("approve-account")]
    public async Task<IActionResult> ApproveAccount([FromBody] ApproveAccountRequest request)
    {
        var command = new ApproveAccountCommand(request.UserId);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpPost("reject-account")]
    public async Task<IActionResult> RejectAccount([FromBody] RejectAccountRequest request)
    {
        var command = new RejectAccountCommand(request.UserId, request.Reason);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [Authorize(Roles = "SuperAdmin")]
    [HttpGet("pending-accounts")]
    public async Task<IActionResult> GetPendingAccounts()
    {
        var query = new GetPendingAccountsQuery();
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var command = new ForgotPasswordCommand(request.Email);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var command = new ResetPasswordCommand(request.Email, request.Otp, request.NewPassword);
        var result = await _mediator.Send(command);
        return HandleResult(result);
    }
}
