using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Employees;
using Methaq.Domain.Lectures;
using Methaq.Domain.QuranCenters;
using Methaq.Domain.Sections.enums;
using Methaq.Domain.Students;

namespace Methaq.Domain.Sections;

public class Section : BaseEntity
{
    public string Name { get; private set; } = null!;
    public AcademicLevel AcademicLevel { get; private set; }
    public SectionStatus Status { get; private set; }
    public Guid CenterId { get; private set; }
    public QuranCenter Center { get; private set; } = null!;
    public Guid SupervisorId { get; private set; }
    public Employee Supervisor { get; private set; } = null!;
    public SectionSchedule Schedule { get; private set; } = null!;

    private readonly List<Student> _students = [];
    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

    private readonly List<Lecture> _lectures = [];
    public IReadOnlyCollection<Lecture> Lectures => _lectures.AsReadOnly();

    protected Section() { }

    private Section(string name, AcademicLevel academicLevel, Guid centerId, Guid supervisorId, SectionSchedule schedule)
    {
        Name = name;
        AcademicLevel = academicLevel;
        CenterId = centerId;
        SupervisorId = supervisorId;
        Schedule = schedule;
        Status = SectionStatus.Active;
    }

    public static ErrorOr<Section> Create(string name, AcademicLevel academicLevel, Guid centerId, Guid supervisorId, SectionSchedule schedule)
    {
        if (string.IsNullOrWhiteSpace(name))
            return SectionErrors.NameRequired;

        if (centerId == Guid.Empty)
            return SectionErrors.CenterIdRequired;

        if (supervisorId == Guid.Empty)
            return SectionErrors.SupervisorIdRequired;

        if (schedule == null)
            return SectionErrors.ScheduleRequired;

        var scheduleValidation = schedule.IsValid();
        if (scheduleValidation.IsError)
            return scheduleValidation.Errors;

        return new Section(name, academicLevel, centerId, supervisorId, schedule);

    }
    public ErrorOr<Success> AddStudent(Student student)
    {
        if (student == null)
            return SectionErrors.StudentNull;

        if (Status == SectionStatus.Closed)
            return SectionErrors.SectionClosed;

        if (_students.Any(s => s.Id == student.Id))
            return SectionErrors.StudentExists;

        _students.Add(student);
        student.AssignToSection(this.Id);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> RemoveStudent(Guid studentId)
    {
        var student = _students.FirstOrDefault(s => s.Id == studentId);
        if (student == null)
            return SectionErrors.StudentNotFound;

        _students.Remove(student);
        student.RemoveFromSection();
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> ChangeSupervisor(Guid newSupervisorId)
    {
        if (newSupervisorId == Guid.Empty)
            return SectionErrors.SupervisorEmpty;

        if (newSupervisorId == SupervisorId)
            return SectionErrors.SameSupervisor;

        SupervisorId = newSupervisorId;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Close()
    {
        if (Status == SectionStatus.Closed)
            return SectionErrors.AlreadyClosed;

        Status = SectionStatus.Closed;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> AddLecture(Lecture lecture)
    {
        if (lecture == null)
            return SectionErrors.LectureNull;

        if (Status == SectionStatus.Closed)
            return SectionErrors.SectionClosed;

        _lectures.Add(lecture);
        MarkAsUpdated();
        return Result.Success;
    }

    public int GetStudentCount() => _students.Count;

    
}
