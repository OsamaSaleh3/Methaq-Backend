using ErrorOr;
using MediatR;
using Methaq.Application.Common.Interfaces;
using Methaq.Domain.AttendanceRecords;
using Methaq.Domain.Lectures.enums;

namespace Methaq.Application.AttendanceRecords.Commands.RecordAttendance;

public class RecordAttendanceCommandHandler : IRequestHandler<RecordAttendanceCommand, ErrorOr<Guid>>
{
    private readonly ILectureRepository _lectureRepository;
    private readonly IAttendanceRecordRepository _attendanceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecordAttendanceCommandHandler(ILectureRepository lectureRepository, IAttendanceRecordRepository attendanceRepository, IUnitOfWork unitOfWork)
    {
        _lectureRepository = lectureRepository;
        _attendanceRepository = attendanceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Guid>> Handle(RecordAttendanceCommand request, CancellationToken cancellationToken)
    {
        var lecture=await _lectureRepository.GetByIdWithSectionAsync(request.LectureId);
        if (lecture is null)
            return RecordAttendanceErrors.LectureNotFound;
        
        if(lecture.Status == LectureStatus.Cancelled)
            return RecordAttendanceErrors.LectureCancelled;

        var studentInSection = lecture.Section.Students.Any(s => s.Id == request.StudentId);
        if(!studentInSection)
            return RecordAttendanceErrors.StudentNotInSection;

        var alreadyRecorded=await _attendanceRepository.ExistsAsync(request.LectureId, request.StudentId);
        if(alreadyRecorded)
            return RecordAttendanceErrors.AlreadyRecorded;

        var attendanceRecordResult = AttendanceRecord.Create(
            request.StudentId,
            request.LectureId,
            request.Status,
            request.ExcuseReason,
            request.Notes
            );
        if(attendanceRecordResult.IsError)
            return attendanceRecordResult.Errors;

        var attendanceRecord = attendanceRecordResult.Value;
        await _attendanceRepository.AddAsync(attendanceRecord);
        await _unitOfWork.SaveChangesAsync();
        return attendanceRecord.Id;

    }
}
