using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Reactivate
{
    public class ReactivateCommandValidator:AbstractValidator<ReactivateCommand>
    {
        public ReactivateCommandValidator()
        {
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Supervisor Id is required");
        }
    }
}
