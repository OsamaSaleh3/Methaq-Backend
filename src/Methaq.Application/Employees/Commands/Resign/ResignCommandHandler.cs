using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.Employees.enums;
using Methaq.Domain.Notifications.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Methaq.Application.Employees.Commands.Resign
{
    public class ResignCommandHandler : IRequestHandler<ResignCommand, ErrorOr<Success>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public ResignCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _notificationService = notificationService;
        }

        public async Task<ErrorOr<Success>> Handle(ResignCommand request, CancellationToken cancellationToken)
        {
            var supervisor = await _employeeRepository.GetByIdWithDetailsAsync(request.EmployeeId);
            if (supervisor is null)
                return ResignErrors.SupervisorNotFound;

            if (!supervisor.CanBeSupervisor())
                return ResignErrors.SupervisorNotActive;

            if (supervisor.SupervisedSections.Count > 0)
                return ResignErrors.SupervisorHasSupervisedSections;

            if (supervisor.IsManager())
            {
                if(supervisor.CenterId is not null)
                    return ResignErrors.SupervisorIsCenterManager;
            }

            var resignResult=supervisor.Resign();
            if (resignResult.IsError)
                return resignResult.Errors;

            await _unitOfWork.SaveChangesAsync();

            await _notificationService.SendAsync(
                supervisor.UserId,
                "تم تسجيل استقالتك",
                $"تم تسجيل استقالتك من المنظومة",
                NotificationType.EmployeeResigned,
                supervisor.Id);

            return Result.Success;
        }
    }
}
