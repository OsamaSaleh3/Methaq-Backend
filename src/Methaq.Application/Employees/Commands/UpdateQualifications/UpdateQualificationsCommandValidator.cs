using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.UpdateQualifications
{
    public class UpdateQualificationsCommandValidator:AbstractValidator<UpdateQualificationsCommand>
    {
        public UpdateQualificationsCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
             .NotEmpty().WithMessage("Supervisor ID is required.");

            RuleFor(x => x.Degree)
           .IsInEnum().WithMessage("Invalid academic degree.");

            RuleFor(x => x.Specialization)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.IslamicQualifications));
            
            RuleFor(x => x.IslamicQualifications)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.IslamicQualifications));

        }
    }
}
