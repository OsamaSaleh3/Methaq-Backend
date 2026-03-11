using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^07[789]\d{7}$").WithMessage("Invalid Jordanian phone number.");
        }
    }
}
