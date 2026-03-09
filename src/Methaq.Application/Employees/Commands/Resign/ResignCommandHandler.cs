using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Employees.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Resign
{
    public class ResignCommandHandler : IRequestHandler<ResignCommand, ErrorOr<Success>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ResignCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<Success>> Handle(ResignCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (supervisor is null)
                return ReactivateErrors.SupervisorNotFound;

            if (!supervisor.CanBeSupervisor())
                return ReactivateErrors.SupervisorNotActive;

            var resignResult=supervisor.Resign();
            if (resignResult.IsError)
                return resignResult.Errors;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success;
        }
    }
}
