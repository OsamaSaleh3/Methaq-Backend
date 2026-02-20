using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Employees;
using Methaq.Domain.Sections.enums;
using Methaq.Domain.Students;

namespace Methaq.Domain.Sections;

public class Section : BaseEntity
{
    public string Name { get; private set; } = null!;
    public AcademicLevel AcademicLevel { get; private set; }

    public Guid SupervisorId { get; private set; }
    public Employee Supervisor { get; private set; } = null!;

    private readonly List<Student> _students = [];
    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

    protected Section() { }

    private Section(string name, AcademicLevel academicLevel, Guid supervisorId)
    {
        Name = name;
        AcademicLevel = academicLevel;
        SupervisorId = supervisorId;
    }

    public static ErrorOr<Section> Create(string name, AcademicLevel academicLevel, Guid supervisorId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return SectionErrors.NameRequired;

        if (supervisorId == Guid.Empty)
            return SectionErrors.SupervisorIdRequired;

        return new Section(name, academicLevel, supervisorId);
    }

    public ErrorOr<Success> AddStudent(Student student)
    {
        if (student == null)
            return SectionErrors.StudentNull;

        if (_students.Any(s => s.UserId == student.UserId))
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

    public int GetStudentCount() => _students.Count;

    public decimal? GetSectionAverageScore()
    {
        var allScores = _students
            .Select(s => s.GetAverageScore())
            .Where(s => s.HasValue)
            .Select(s => s is not null ? s.Value : 0)
            .ToList();

        return allScores.Count != 0 ? allScores.Average() : null;
    }
}