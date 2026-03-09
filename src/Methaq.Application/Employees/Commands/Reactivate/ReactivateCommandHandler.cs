using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Employees.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Reactivate
{
    public class ReactivateCommandHandler : IRequestHandler<ReactivateCommand, ErrorOr<Success>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReactivateCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<Success>> Handle(ReactivateCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (supervisor is null)
                return ReactivateErrors.SupervisorNotFound;

            if (supervisor.CanBeSupervisor())
                return ReactivateErrors.SupervisorAlreadyActive;

            var reactivateResult=supervisor.Reactivate();
            if (reactivateResult.IsError)
                return reactivateResult.Errors;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success;
        }
    }
}
