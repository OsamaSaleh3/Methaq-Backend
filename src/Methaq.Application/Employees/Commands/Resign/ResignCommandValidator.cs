using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Resign
{
    public class ResignCommandValidator:AbstractValidator<ResignCommand>
    {
        public ResignCommandValidator() {
            RuleFor(x => x.EmployeeId).NotEmpty().WithMessage("Supervisor Id is required");

        }
    }
}
