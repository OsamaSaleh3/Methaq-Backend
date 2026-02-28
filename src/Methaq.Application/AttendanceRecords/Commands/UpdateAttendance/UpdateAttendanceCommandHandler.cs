using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Methaq.Application.AttendanceRecords.Commands.UpdateAttendance;

public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, ErrorOr<Success>>
{
    private readonly IAttendanceRecordRepository _attendanceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAttendanceCommandHandler(IAttendanceRecordRepository attendanceRepository, IUnitOfWork unitOfWork)
    {
        _attendanceRepository = attendanceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var record = await _attendanceRepository.GetByIdAsync(request.AttendanceRecordId);
        if (record is null)
            return UpdateAttendanceErrors.NotFound;

        var updateResult=record.UpdateStatus(
            request.Status,
            request.ExcuseReason,
            request.Notes
            );
        if(updateResult.IsError)
            return updateResult.Errors;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
