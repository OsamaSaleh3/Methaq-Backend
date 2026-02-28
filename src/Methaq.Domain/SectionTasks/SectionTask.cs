using ErrorOr;
using Methaq.Domain.Common;
using Methaq.Domain.Employees;
using Methaq.Domain.Sections;
using Methaq.Domain.SectionTasks.enums;
using Methaq.Domain.SectionTasks.ValueObject;
using Methaq.Domain.Students;

namespace Methaq.Domain.SectionTasks;

public class SectionTask : BaseEntity
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public QuranRange? Range { get; private set; }

    public Guid SectionId { get; private set; }
    public Section Section { get; private set; } = null!;
    public enums.TaskStatus Status { get; private set; } = enums.TaskStatus.Pending;
    public TaskTypes Types { get; private set; } 
    public Guid LectureId { get; private set; }
    public Guid AssignedById { get; private set; }
    public Employee AssignedBy { get; private set; } = null!;
    public decimal FullMark { get; private set; }
    public Guid? StudentId { get; private set; }
    public Student? Student { get; private set; }

    private readonly List<StudentTaskEvaluation> _evaluations = [];
    public IReadOnlyCollection<StudentTaskEvaluation> Evaluations => _evaluations.AsReadOnly();

    protected SectionTask() { }

    private SectionTask(string title, string? description, Guid sectionId, Guid lectureId, Guid assignedById, decimal fullMark)
    {
        Title = title;
        Description = description;
        SectionId = sectionId;
        LectureId = lectureId;
        AssignedById = assignedById;
        FullMark = fullMark;
    }

    public static ErrorOr<SectionTask> Create(string title, string? description, Guid sectionId, Guid lectureId, Guid assignedById, decimal fullMark = 100)
    {
        if (string.IsNullOrWhiteSpace(title))
            return SectionTaskErrors.TitleRequired;

        if (sectionId == Guid.Empty)
            return SectionTaskErrors.SectionIdRequired;

        if (lectureId == Guid.Empty)
            return SectionTaskErrors.LectureIdRequired;

        if (assignedById == Guid.Empty)
            return SectionTaskErrors.AssignedByIdRequired;

        if (fullMark <= 0)
            return SectionTaskErrors.InvalidFullMark;

        return new SectionTask(title, description, sectionId, lectureId, assignedById, fullMark);
    }

    public ErrorOr<Success> EvaluateStudent(Guid studentId, decimal mark, string? notes)
    {
        if (mark < 0 || mark > FullMark)
            return SectionTaskErrors.InvalidMark(FullMark);

        var existing = _evaluations.FirstOrDefault(e => e.StudentId == studentId);
        if (existing != null)
        {
            existing.Update(mark, notes);
        }
        else
        {
            _evaluations.Add(new StudentTaskEvaluation(studentId, this.Id, mark, notes));
        }

        MarkAsUpdated();
        return Result.Success;
    }

    public bool IsForWholeSection() => StudentId is null;
    public bool IsForSpecificStudent() => StudentId is not null;
}

public class StudentTaskEvaluation
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;
    public Guid SectionTaskId { get; private set; }
    public SectionTask SectionTask { get; private set; } = null!;
    public decimal AchievedMark { get; private set; }
    public string? Notes { get; private set; }
    public DateTime EvaluatedAt { get; private set; }

    protected StudentTaskEvaluation() { }

    public StudentTaskEvaluation(Guid studentId, Guid sectionTaskId, decimal mark, string? notes)
    {
        StudentId = studentId;
        SectionTaskId = sectionTaskId;
        AchievedMark = mark;
        Notes = notes;
        EvaluatedAt = DateTime.UtcNow;
    }

    public void Update(decimal mark, string? notes)
    {
        AchievedMark = mark;
        Notes = notes;
        EvaluatedAt = DateTime.UtcNow;
    }
}
