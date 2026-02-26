using ErrorOr;
using Methaq.Domain.AttendanceRecords;
using Methaq.Domain.Common;
using Methaq.Domain.Lectures.enums;
using Methaq.Domain.SectionTasks;
using Methaq.Domain.Sections;

namespace Methaq.Domain.Lectures;

public class Lecture : BaseEntity
{
    public Guid SectionId { get; private set; }
    public Section Section { get; private set; } = null!;

    public DateTime Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }

    public string? Notes { get; private set; }
    public LectureStatus Status { get; private set; }

    private readonly List<SectionTask> _SectionTasks = [];
    public IReadOnlyCollection<SectionTask> SectionTasks => _SectionTasks.AsReadOnly();

    private readonly List<AttendanceRecord> _attendanceRecords = [];
    public IReadOnlyCollection<AttendanceRecord> AttendanceRecords => _attendanceRecords.AsReadOnly();

    protected Lecture() { }

    private Lecture(Guid sectionId, DateTime date, TimeOnly startTime, TimeOnly endTime)
    {
        SectionId = sectionId;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Status = LectureStatus.Scheduled;
    }

    public static ErrorOr<Lecture> Create(Guid sectionId, DateTime date, TimeOnly startTime, TimeOnly endTime)
    {
        if (sectionId == Guid.Empty)
            return LectureErrors.SectionIdRequired;

        if (date.Date < DateTime.UtcNow.Date)
            return LectureErrors.DateCannotBeInPast;

        if (endTime <= startTime)
            return LectureErrors.InvalidTimeRange;

        return new Lecture(sectionId, date, startTime, endTime);
    }

    public ErrorOr<Success> AddAttendanceRecord(AttendanceRecord record)
    {
        if (record == null)
            return LectureErrors.AttendanceRecordNull;

        if (Status == LectureStatus.Cancelled)
            return LectureErrors.LectureCancelled;

        if (_attendanceRecords.Any(a => a.StudentId == record.StudentId))
            return LectureErrors.AttendanceAlreadyRecorded;

        _attendanceRecords.Add(record);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> AddSectionTask(SectionTask task)
    {
        if (task == null)
            return LectureErrors.TaskNull;

        if (Status == LectureStatus.Cancelled)
            return LectureErrors.LectureCancelled;

        _SectionTasks.Add(task);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Start()
    {
        if (Status != LectureStatus.Scheduled)
            return LectureErrors.CannotStartLecture;

        Status = LectureStatus.InProgress;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Complete(string? notes = null)
    {
        if (Status == LectureStatus.Completed)
            return LectureErrors.AlreadyCompleted;

        if (Status == LectureStatus.Cancelled)
            return LectureErrors.LectureCancelled;

        Status = LectureStatus.Completed;
        Notes = notes;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Cancel()
    {
        if (Status == LectureStatus.Completed)
            return LectureErrors.CannotCancelCompleted;

        Status = LectureStatus.Cancelled;
        MarkAsUpdated();
        return Result.Success;
    }
}
