using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Application.Sections.Commands.CreateSection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.UpdateQualifications
{
    public class UpdateQualificationsCommandHandler : IRequestHandler<UpdateQualificationsCommand, ErrorOr<Success>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateQualificationsCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateQualificationsCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (supervisor is null)
                return UpdateQualificationsErrors.SupervisorNotFound;

            if (!supervisor.CanBeSupervisor())
                return UpdateQualificationsErrors.SupervisorNotActive;

            var updateQualificationResult = supervisor.UpdateQualifications(
                request.Degree,
                request.Specialization,
                request.IslamicQualifications
                );

            if (updateQualificationResult.IsError)
                return updateQualificationResult.Errors;


            await _unitOfWork.SaveChangesAsync();

            return Result.Success;

        }
    }
}
